using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    Vector3 enemyToPlayerVector;
    public double enemyToPlayerDistance;
    public double enemyToPlayerAngle;

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

    public bool swapped = false;
	public bool searched = false;

    private IEnumerator turnCoroutine;
    private IEnumerator patrolCoroutine;

    public Quaternion startRotation;

    public NavMeshAgent agent;
    
    // Use this for initialization
    void Start () {

        startPos = transform.position;
        //endPos = new Vector3(startPos.x, startPos.y, startPos.z + 5);
		endPos = startPos + (transform.forward * patrolDistance);

        //Following 2 line of code from https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
        //startTime = Time.time;

        //journeyLength = Vector3.Distance(startPos, endPos);

        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGO.GetComponent<CharacterControl>();

        GM = GameObject.FindGameObjectWithTag("GM");
        gameOverScript = GM.GetComponent<gameOverText>();

        agent = GetComponent<NavMeshAgent>();

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
			if(transform.position.x != endPos.x || transform.position.z != endPos.z)
            {
				transform.LookAt(new Vector3(endPos.x, transform.position.y, endPos.z));
				moveToPoint(new Vector3(endPos.x, transform.position.y, endPos.z));
                swapped = false;

            }
			else if(transform.position.x == endPos.x && transform.position.z == endPos.z && swapped == false)
            {
                Debug.Log("Turning");
                swapPoints();

                transform.rotation *= Quaternion.Euler(0f, 180f, 0f);
                Debug.Log("Turned");
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
					playerPos = playerGO.transform.position;
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

			if (enemyToPlayerDistance <= 10 && enemyToPlayerAngle <= 45) 
			{
				RaycastHit hit;

				if (Physics.Raycast (transform.position, enemyToPlayerVector, out hit, 10f) && hit.transform.CompareTag ("Player")) {
					currentTransition = Transition.playerSeen;
				}
			}

//			else if (enemyToPlayerDistance > 20) 
//			{
//				currentTransition = Transition.findNothing;
//			}
            
            //caution transition end

            //caution actions start
//			while(transform.position.x != playerPos.x || transform.position.z != transform.position.z)
//			{
//				
//			}

			if ((transform.position.x != playerPos.x || transform.position.z != transform.position.z) && !searched) 
			{
				moveToPoint(new Vector3(playerPos.x, transform.position.y, playerPos.z));
				Debug.Log ("Moving to player");
			} 

			else 
			{
				searched = true;

				if(transform.position.x == playerPos.x && transform.position.z == transform.position.z)
				{
					Debug.Log ("At playerPos");
					transform.LookAt(new Vector3(startPos.x, transform.position.y, startPos.z));
					moveToPoint(new Vector3(startPos.x, transform.position.y, startPos.z));
				}

				//moveToPoint(startPos);

				if(transform.position.x == startPos.x && transform.position.z == transform.position.z)
				{
					searched = false;
					currentTransition = Transition.findNothing;
				}
			}
 
        }
    }

    private void moveToPoint(Vector3 point)
    {
        //navmesh
        agent = GetComponent<NavMeshAgent>();
        agent.destination = point;
    }

    private void gameOver(String outcome)
    {
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
        return playerGO.transform.position - transform.position;
    }

    double getEnemyToPlayerDistance()
    {
        return Math.Sqrt((enemyToPlayerVector.x * enemyToPlayerVector.x) + (enemyToPlayerVector.y * enemyToPlayerVector.y) + (enemyToPlayerVector.z * enemyToPlayerVector.z));
    }

    double getAngleToPlayer()
    {
        return Vector3.Angle(getEnemytoPlayerVector(), transform.forward);
    }

    void swapPoints()
    {
        tempPos = startPos;
        startPos = endPos;
        endPos = tempPos;
        swapped = true;
    }
}