using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    Vector3 enemyToPlayerVector;
    double enemyToPlayerDistance;
    double enemyToPlayerAngle;

    public GameObject playerGO;
    public GameObject GM;
    CharacterControl playerScript;
    gameOverText gameOverScript;

    enum State { patrolling, caution, alert };
    State currentState = State.patrolling;
    State previousState;

    enum Transition { playerSeen, seeSomething, findNothing};
    Transition currentTransition = Transition.findNothing;

    //Following 5 lines of code from https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
    public Vector3 startPos;
    public Vector3 endPos;
	public float patrolDistance = 10;
    private Vector3 tempPos;

    public Vector3 playerPos;

    public float speed = 1.0f;

    private float startTime;

    public float waitTime = 1.0f;

    private float journeyLength;

    private bool swapped = false;
    private bool turned = true;

    private IEnumerator turnCoroutine;
    private IEnumerator patrolCoroutine;

    public Quaternion startRotation;

    //public NavMeshAgent agent;
    
    // Use this for initialization
    void Start () {

        startPos = transform.position;
        //endPos = new Vector3(startPos.x, startPos.y, startPos.z + 5);
		endPos = startPos + (transform.forward * patrolDistance);

        //Following 2 line of code from https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        startTime = Time.time;

        journeyLength = Vector3.Distance(startPos, endPos);

        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGO.GetComponent<CharacterControl>();

        GM = GameObject.FindGameObjectWithTag("GM");
        gameOverScript = GM.GetComponent<gameOverText>();

        //agent = GetComponent<NavMeshAgent>();

        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update () {

        enemyToPlayerVector = getEnemytoPlayerVector();
        enemyToPlayerDistance = getEnemyToPlayerDistance();
        enemyToPlayerAngle = getAngleToPlayer();

        Debug.DrawRay(transform.position, enemyToPlayerVector, Color.red);

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

                        case Transition.seeSomething:
                            previousState = State.patrolling;
                            currentState = State.caution;
                            break;

                        default:
                            break;
                    }
                    break;
                }

            //case State.alert:
            //    {
            //        switch(currentTransition)
            //        {
            //            case Transition.playerLost:
            //                previousState = State.alert;
            //                currentState = State.caution;
            //                break;

            //            default:
            //                break;
            //        }
            //        break;
            //    }

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
                //patrolCoroutine = patrol(waitTime);
                //StartCoroutine(patrolCoroutine);

                //float distCovered = (Time.time - startTime) * speed;

                //float fracJourney = distCovered / journeyLength;
                //yield return new WaitForSeconds(wait);
                transform.LookAt(endPos);
                moveToPoint(endPos);
                //transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
                swapped = false;

            }
            else if(transform.position == endPos && swapped == false)
            {
                //turnToEndPos();

                if (turned)
                {
                    turned = false;
                    Debug.Log("Turning");
                    swapPoints();

                    transform.rotation *= Quaternion.Euler(0f, 180f, 0f);

                    turned = true;
                    Debug.Log("Turned");
                    waitTime = UnityEngine.Random.Range(0.5f, 2.0f);
                    speed = UnityEngine.Random.Range(0.5f, 1.5f);
                }

                //turnCoroutine = turnToEndPos(waitTime);
                //StartCoroutine(turnCoroutine);
                //updatePatrol();


            }

            //patrolling actions end

            //patrolling transition start
            if (enemyToPlayerDistance <= 10 && enemyToPlayerAngle <= 45)
            {
                //Debug.Log("Player in enemy scope, obstructed");

                RaycastHit hit;
                if (Physics.Raycast(transform.position, enemyToPlayerVector, out hit, 10f) && hit.transform.CompareTag("Player"))
                {
                    Debug.Log("Enemy Sighted!");
                    currentTransition = Transition.playerSeen;
                }
            }

            else if((enemyToPlayerDistance > 10 && enemyToPlayerDistance <= 20))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, enemyToPlayerVector, out hit, 20f) && hit.transform.CompareTag("Player"))
                {
                    Debug.Log("SEE SOMETHING");
                    currentTransition = Transition.seeSomething;
                }
            }
            //patrolling transition end

            //Debug.Log("End of patrolling");
        }

        else if(currentState == State.alert)
        {
            ////alert actions start
            
            ////agent.SetDestination(playerGO.transform.position);

            ////chase(playerGO.transform);

            gameOver("L");

        }

        else//state.caution
        {
            //caution transition start

            checkForPlayer();
            
            //caution transition end

            //caution actions start

            playerPos = playerGO.transform.position;
            moveToPoint(playerPos);

            if(transform.position == playerPos)
            {
                lookAround();
            }

            moveToPoint(startPos);

            if(transform.position == startPos)
            {
                currentTransition = Transition.findNothing;
            }
            
        }
    }

    private void checkForPlayer()
    {
        //throw new NotImplementedException();
        if (enemyToPlayerDistance <= 10 && enemyToPlayerAngle <= 45)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, enemyToPlayerVector, out hit, 10f) && hit.transform.CompareTag("Player"))
            {
                currentTransition = Transition.playerSeen;
            }

        }
    }

    private void lookAround()
    {
        //throw new NotImplementedException();

        //look left code

        checkForPlayer();

        //look right code

        checkForPlayer();
    }

    private void moveToPoint(Vector3 point)
    {
        //throw new NotImplementedException();

        //navmesh
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = point;
    }

    private void gameOver(String outcome)
    {
        //throw new NotImplementedException();
        if(outcome == "L")
        {
            gameOverScript.gameOver();
        }

        else
        {
            gameOverScript.win();
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
        transform.LookAt(endPos);
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
