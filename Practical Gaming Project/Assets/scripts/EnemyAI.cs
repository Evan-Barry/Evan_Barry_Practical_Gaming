using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    Vector3 enemyToPlayerVector;
    double enemyToPlayerDistance;
    double enemyToPlayerAngle;

    GameObject playerGO;
    GameObject GM;
    GameObject spotlight;
    CharacterControl playerScript;
    timer timerScript;
    Light spot;

    enum State { patrolling, caution, alert };
    State currentState = State.patrolling;
    State previousState;

    enum Transition { playerSeen, playerLost, hearSomething, findNothing};
    Transition currentTransition = Transition.findNothing;

    //Following 5 lines of code from https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
    public Vector3 startPos;
    public Vector3 endPos;
    private Vector3 tempPos;

    public float speed = 1.0f;

    private float startTime;

    public float waitTime = 1.0f;

    private float journeyLength;

    private bool swapped = false;
    private bool turned = true;

    private IEnumerator turnCoroutine;
    private IEnumerator patrolCoroutine;

    //public NavMeshAgent agent;
    
    // Use this for initialization
    void Start () {

        startPos = transform.position;
        //endPos = new Vector3(startPos.x, startPos.y, startPos.z + 5);
        endPos = startPos + (transform.forward * 5);

        //Following 2 line of code from https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        startTime = Time.time;

        journeyLength = Vector3.Distance(startPos, endPos);

        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGO.GetComponent<CharacterControl>();

        GM = GameObject.FindGameObjectWithTag("GM");
        timerScript = GM.GetComponent<timer>();

        spotlight = GameObject.FindGameObjectWithTag("EnemySpotLight");
        spot = spotlight.GetComponent<Light>();
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
                            previousState = State.patrolling;
                            currentState = State.alert;
                            break;

                        case Transition.hearSomething:
                            previousState = State.patrolling;
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
                            previousState = State.alert;
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
                            previousState = State.caution;
                            currentState = State.patrolling;
                            break;

                        case Transition.playerSeen:
                            previousState = State.caution;
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
            //patrolling actions start
            if(transform.position != endPos)
            {
                patrolCoroutine = patrol(waitTime);
                StartCoroutine(patrolCoroutine);
                
            }
            else if(transform.position == endPos && swapped == false)
            {
                //turnToEndPos();
                turnCoroutine = turnToEndPos(waitTime);
                StartCoroutine(turnCoroutine);
                updatePatrol();
            }

            spot.range = 10;
            spot.spotAngle = 90;
            //patrolling actions end

            //patrolling transition start
            if(enemyToPlayerDistance <= 10 && enemyToPlayerAngle <= 45)
            {
                Debug.Log("Enemy Sighted!");
                currentTransition = Transition.playerSeen;
                playerScript.seen = true;
            }

            else if((enemyToPlayerDistance > 10 && enemyToPlayerDistance <= 15) && playerScript.currentStance == CharacterControl.stance.standing)
            {
                Debug.Log("SOMETHING HEARD");
                currentTransition = Transition.hearSomething;
                playerScript.seen = false;
            }
            //patrolling transition end
        }

        else if(currentState == State.alert)
        {
            //alert transition start
            //playerLost if player is not seen by enemy for 10 seconds
            if(!playerScript.seen)
            {
                timerScript.timerRunning = true;
                timerScript.alertCountdown();
            }

            else
            {
                transform.LookAt(new Vector3(playerGO.transform.position.x, playerGO.transform.position.y + 0.5f, playerGO.transform.position.z));
            }

            if (enemyToPlayerDistance > 10 || enemyToPlayerAngle > 45)
            {
                playerScript.seen = false;
            }

            if(enemyToPlayerDistance <= 10 && enemyToPlayerAngle <= 45)
            {
                playerScript.seen = true;
                timerScript.timerRunning = false;
                currentTransition = Transition.playerSeen;
            }

            if(timerScript.alertTime <= 0f)
            {
                currentTransition = Transition.playerLost;
            }
            //transition complete

            //alert actions start
            spot.range = 10f;
            spot.spotAngle = 90f;

            

            //agent.SetDestination(playerGO.transform.position);

            //chase(playerGO.transform);
        }

        else//state.caution
        {
            //caution transition start
        
            if(!playerScript.seen)
            {
                timerScript.timerRunning = true;
                timerScript.cautionCountdown();
            }

            if(enemyToPlayerDistance > 15 || enemyToPlayerAngle > 67.5)
            {
                playerScript.seen = false;
            }

            if(enemyToPlayerDistance <= 15 && enemyToPlayerAngle <= 67.5)
            {
                playerScript.seen = true;
                timerScript.timerRunning = false;
                currentTransition = Transition.playerSeen;
            }

            if(timerScript.cautionTime <= 0f)
            {
                currentTransition = Transition.findNothing;
            }
            //caution transition end

            //caution actions start
            spot.range = 15;
            spot.spotAngle = 135;

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
        transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
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

        journeyLength = Vector3.Distance(startPos, endPos);
    }

    void chase(Transform playerPos)
    {
        float distCovered = (Time.time - startTime) * speed;

        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, playerPos.position, fracJourney);
    }

    IEnumerator turnToEndPos(float wait)
    {
        if(turned)
        {
            turned = false;
            Debug.Log("Turning");
            yield return new WaitForSeconds(wait);
            swapPoints();

            transform.rotation *= Quaternion.Euler(0f, 180f, 0f);

            turned = true;
            Debug.Log("Turned");
            waitTime = UnityEngine.Random.Range(0.5f, 2.0f);
            speed = UnityEngine.Random.Range(0.5f, 1.5f);
        }
    }


}
