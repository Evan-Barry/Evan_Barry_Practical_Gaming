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
	spawnEnemies spawnEnemiesScript;

    enum State { patrolling, caution, alert };
    State currentState = State.patrolling;
    State previousState;

	public bool readyToPatrol = false;

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

	Animator anim;
    
    // Use this for initialization
    void Start () {
        
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerGO.GetComponent<CharacterControl>();

        GM = GameObject.FindGameObjectWithTag("GM");
        gameOverScript = GM.GetComponent<gameOverText>();
		spawnEnemiesScript = GM.GetComponent<spawnEnemies>();

        agent = GetComponent<NavMeshAgent>();

        startRotation = transform.rotation;

		anim = GetComponent<Animator>();

		startPos = transform.position;
		endPos = startPos + (transform.forward * patrolDistance);
    }

    // Update is called once per frame
    void Update () {

        enemyToPlayerVector = getEnemytoPlayerVector();
        enemyToPlayerDistance = getEnemyToPlayerDistance();
        enemyToPlayerAngle = getAngleToPlayer();

        Debug.DrawRay(transform.position + new Vector3(0,1,0), enemyToPlayerVector, Color.red);

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

		if (currentState == State.patrolling) {
			//patrolling actions start

			if (transform.position.x != endPos.x || transform.position.z != endPos.z) {
				transform.LookAt (new Vector3 (endPos.x, transform.position.y, endPos.z));
				moveToPoint (new Vector3 (endPos.x, transform.position.y, endPos.z));
				swapped = false;

			} else if (transform.position.x == endPos.x && transform.position.z == endPos.z && swapped == false) {
				Debug.Log ("Turning");
				swapPoints ();

				transform.rotation *= Quaternion.Euler (0f, 180f, 0f);
				Debug.Log ("Turned");
			}

			//patrolling actions end

			//patrolling transition start
			if (enemyToPlayerDistance <= 5 && enemyToPlayerAngle <= 45) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position + new Vector3 (0, 1, 0), enemyToPlayerVector, out hit, 5f) && hit.transform.CompareTag ("Player")) {
					Debug.Log ("Enemy Sighted!");
					currentTransition = Transition.playerSeen;
					Debug.Log (hit.collider.gameObject.name);
				}
			} else if ((enemyToPlayerDistance > 5 && enemyToPlayerDistance <= 10) && enemyToPlayerAngle <= 45) {
				RaycastHit hit;
				if (Physics.Raycast (transform.position + new Vector3 (0, 1, 0), enemyToPlayerVector, out hit, 10f) && hit.transform.CompareTag ("Player")) {
					Debug.Log ("SEE SOMETHING");
					currentTransition = Transition.seeSomething;
					playerPos = playerGO.transform.position;
					Debug.Log (hit.collider.gameObject.name);
				}
			}
			//patrolling transition end
		} else if (currentState == State.alert) {
			gameOverScript.gameOver ();
		} else {//state.caution
			//caution transition start

			if (enemyToPlayerDistance <= 5 && enemyToPlayerAngle <= 45) {
				RaycastHit hit;

				if (Physics.Raycast (transform.position + new Vector3 (0, 1, 0), enemyToPlayerVector, out hit, 5f) && hit.transform.CompareTag ("Player")) {
					currentTransition = Transition.playerSeen;
					Debug.Log (hit.collider.gameObject.name);
				}
			}

			if ((transform.position.x != playerPos.x || transform.position.z != transform.position.z) && !searched) {
				moveToPoint (new Vector3 (playerPos.x, transform.position.y, playerPos.z));
				Debug.Log ("Moving to player");

				RaycastHit hit;

				if (Physics.Raycast (transform.position + new Vector3 (0, 1, 0), enemyToPlayerVector, out hit, 10f) && hit.transform.CompareTag ("walkthroughWall")) {
					returnToStartPos ();
					Debug.Log (hit.collider.gameObject.name);
				}
			} else {
				returnToStartPos ();
			}
 
		}

		if (transform.position == startPos) 
		{
			readyToPatrol = true;
		}

    }

    private void returnToStartPos()
    {
        searched = true;

        if (transform.position.x == playerPos.x && transform.position.z == transform.position.z)
        {
            transform.LookAt(new Vector3(startPos.x, transform.position.y, startPos.z));
            moveToPoint(new Vector3(startPos.x, transform.position.y, startPos.z));
        }

        if (transform.position.x == startPos.x && transform.position.z == transform.position.z)
        {
            searched = false;
            currentTransition = Transition.findNothing;
        }
    }

    private void moveToPoint(Vector3 point)
    {
        //navmesh
        agent = GetComponent<NavMeshAgent>();
        agent.destination = point;
		anim.SetBool("Moving", true);
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
		anim.SetBool("Moving", false);
        tempPos = startPos;
        startPos = endPos;
        endPos = tempPos;
        swapped = true;
    }
}