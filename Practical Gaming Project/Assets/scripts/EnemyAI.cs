using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    Vector3 enemyToPlayerVector;
    double enemyToPlayerDistance;
    double enemyToPlayerAngle;

    public GameObject playerGO;
    private CharacterControl playerScript;

    enum State { patrolling, caution, alert };
    State currentState = State.patrolling;

    enum Transition { playerSeen, playerLost, hearSomething, findNothing};
    Transition currentTransition = Transition.findNothing;

    //Following 5 lines of code from https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
    public Transform startPos;
    public Transform endPos;
    public Transform tempPos;

    public float speed = 1.0f;

    private float startTime;

    public float waitTime = 1.0f;

    private float journeyLength;

    private bool swapped = false;
    private bool turned = true;

    private IEnumerator turnCoroutine;
    private IEnumerator patrolCoroutine;

    private void Awake()
    {
        playerGO = GameObject.Find("playerCube");
        playerScript = playerGO.GetComponent<CharacterControl>();
    }
    // Use this for initialization
    void Start () {
        //Following 2 line of code from https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        startTime = Time.time;

        journeyLength = Vector3.Distance(startPos.position, endPos.position);
        
    }
	
	// Update is called once per frame
	void Update () {

        enemyToPlayerVector = getEnemytoPlayerVector();
        enemyToPlayerDistance = getEnemyToPlayerDistance();
        enemyToPlayerAngle = getAngleToPlayer();

        switch (currentState)
        {
            case State.patrolling:
                {
                    switch(currentTransition)
                    {
                        case Transition.playerSeen:
                            currentState = State.alert;
                            break;

                        case Transition.hearSomething:
                            currentState = State.caution;
                            break;

                        default:
                            break;
                    }
                    break;
                }

            case State.alert:
                {
                    switch(currentTransition)
                    {
                        case Transition.playerLost:
                            currentState = State.caution;
                            break;

                        default:
                            break;
                    }
                    break;
                }

            case State.caution:
                {
                    switch(currentTransition)
                    {
                        case Transition.findNothing:
                            currentState = State.patrolling;
                            break;

                        case Transition.playerSeen:
                            currentState = State.alert;
                            break;

                        default:
                            break;
                    }
                    break;
                }

        }

        Debug.Log("Enemy State = " + currentState);
        Debug.Log("Enemy Transition = " + currentTransition);

        if(currentState == State.patrolling)
        {
            //transform.position += transform.forward * Time.deltaTime;
            if(transform.position != endPos.position)
            {
                patrolCoroutine = patrol(waitTime);
                StartCoroutine(patrolCoroutine);
                
            }
            else if(transform.position == endPos.position && swapped == false)
            {
                //turnToEndPos();
                turnCoroutine = turnToEndPos(waitTime);
                StartCoroutine(turnCoroutine);
                updatePatrol();
            }

            if(enemyToPlayerDistance <= 10 && enemyToPlayerAngle <= 45)
            {
                Debug.Log("Enemy Sighted!");
                currentTransition = Transition.playerSeen;
            }

            else if((enemyToPlayerDistance > 10 && enemyToPlayerDistance <= 15) && playerScript.currentStance == CharacterControl.stance.standing)
            {
                Debug.Log("SOMETHING HEARD");
                currentTransition = Transition.hearSomething;
            }
        }

        else if(currentState == State.alert)
        {
            
        }

        else//state.caution
        {

        }
    }

    Vector3 getEnemytoPlayerVector()
    {
        //Debug.Log("Enemy to Player Vector" + enemyToPlayerVector);

        return playerGO.transform.position - transform.position;
    }

    double getEnemyToPlayerDistance()
    {
        //Debug.Log("Enemy to Player Distance - " + enemyToPlayerDistance);

        return Math.Sqrt((enemyToPlayerVector.x * enemyToPlayerVector.x) + (enemyToPlayerVector.y * enemyToPlayerVector.y) + (enemyToPlayerVector.z * enemyToPlayerVector.z));
    }

    double getAngleToPlayer()
    {
        //Debug.Log("Enemy to Player Angle - " + Vector3.Angle(enemyToPlayerVector, transform.forward));

        return Vector3.Angle(getEnemytoPlayerVector(), transform.forward);
    }

    IEnumerator patrol(float wait)
    {
        //Following 3 line of code from https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        float distCovered = (Time.time - startTime) * speed;

        float fracJourney = distCovered / journeyLength;
        yield return new WaitForSeconds(wait);
        transform.position = Vector3.Lerp(startPos.position, endPos.position, fracJourney);
        swapped = false;
    }

    void swapPoints()
    {
        tempPos = startPos;
        startPos = endPos;
        endPos = tempPos;
        swapped = true;
    }

    void updatePatrol()
    {
        //Following 2 line of code from https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        startTime = Time.time;

        journeyLength = Vector3.Distance(startPos.position, endPos.position);
    }

    IEnumerator turnToEndPos(float wait)
    {
        if(turned)
        {
            turned = false;
            Debug.Log("Turning");
            yield return new WaitForSeconds(wait);
            swapPoints();
            if (transform.rotation.y == 0)
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, 180, transform.rotation.z);
            }
            else
            {
                transform.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
            }
            
            turned = true;
            Debug.Log("Turned");
            waitTime = UnityEngine.Random.Range(0.5f, 2.0f);
            speed = UnityEngine.Random.Range(0.5f, 1.5f);
        }
    }


}
