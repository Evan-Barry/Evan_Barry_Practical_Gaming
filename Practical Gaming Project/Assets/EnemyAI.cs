using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    Vector3 enemyToPlayerVector;
    double enemyToPlayerDistance;
    double enemyToPlayerAngle;

    GameObject playerGO;

    enum State { patrolling, caution, alert };
    State currentState = State.patrolling;

    enum Transition { playerSeen, playerLost, hearSomething, findNothing};
    Transition currentTransition = Transition.findNothing;

	// Use this for initialization
	void Start () {

        playerGO = GameObject.FindGameObjectWithTag("Player");

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
            transform.position += transform.forward * Time.deltaTime;

            if(enemyToPlayerDistance <= 10 && enemyToPlayerAngle <= 45)
            {
                Debug.Log("Enemy Sighted!");
                currentTransition = Transition.playerSeen;
            }
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
}
