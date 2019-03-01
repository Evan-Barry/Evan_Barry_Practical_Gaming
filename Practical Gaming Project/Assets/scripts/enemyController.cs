using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

    Vector3 enemyToPlayerVector;
    double enemyToPlayerDistance;
    double enemyToPlayerAngle;
    
    GameObject playerGO;
    GameObject spotLightGO;
    CharacterControl playerScript;
    

	// Use this for initialization
	void Start () {
        playerGO = GameObject.FindGameObjectWithTag("Player");

        spotLightGO = GameObject.FindGameObjectWithTag("EnemySpotLight");

        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>();
    }
	
	// Update is called once per frame
	void Update () {

        spotLightGO.transform.position = transform.position;
        spotLightGO.transform.rotation = transform.rotation;

        enemyToPlayerVector = getEnemytoPlayerVector();

        enemyToPlayerDistance = getEnemyToPlayerDistance();

        enemyToPlayerAngle = getAngleToPlayer();

        if(enemyToPlayerDistance <= 10 && enemyToPlayerAngle <= 45)
        {
            //enemy sighted
            Debug.Log("ENEMY SIGHTED!");
        }
        else if (enemyToPlayerDistance <= 15 && playerScript.currentStance == CharacterControl.stance.standing && playerScript.moving == true)
        {
            Debug.Log("SOMETHING HEARD");
        }
        else
        {
            Debug.Log("ENEMY NOT SIGHTED");
        }
    }

    void getEnemyForward()
    {
        Debug.Log("Enemy Forward" + transform.forward);
    }

    Vector3 getEnemytoPlayerVector()
    {
        //Debug.Log("Enemy to Player Vector" + enemyToPlayerVector);

        return playerGO.transform.position - transform.position;
    }

    void getEnemyPosition()
    {
        Debug.Log("Enemy Position - " + transform.position);
    }

    void getPlayerPosition()
    {
        Debug.Log("Player Position - " + playerGO.transform.position);
    }

    double getEnemyToPlayerDistance()
    {
        //Debug.Log("Enemy to Player Distance - " + enemyToPlayerDistance);

        return Math.Sqrt((enemyToPlayerVector.x * enemyToPlayerVector.x) + (enemyToPlayerVector.y * enemyToPlayerVector.y) + (enemyToPlayerVector.z * enemyToPlayerVector.z));
    }

    double getAngleToPlayer()
    {
        return Vector3.Angle(enemyToPlayerVector, transform.forward);
    }
}
