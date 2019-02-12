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
    

	// Use this for initialization
	void Start () {
        playerGO = GameObject.FindGameObjectWithTag("Player");

        spotLightGO = GameObject.FindGameObjectWithTag("EnemySpotLight");
            
        
    }
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKey(KeyCode.W))
        //{
        //    transform.position += transform.forward * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    transform.position -= transform.forward * Time.deltaTime;
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Rotate(Vector3.up * -40 * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Rotate(Vector3.up * 40 * Time.deltaTime);
        //}

        spotLightGO.transform.position = transform.position;
        spotLightGO.transform.rotation = transform.rotation;

        

        //getEnemyPosition();

        //getPlayerPosition();

        enemyToPlayerVector = getEnemytoPlayerVector();

        enemyToPlayerDistance = getEnemyToPlayerDistance();

        getEnemyForward();

        enemyToPlayerAngle = getAngleToPlayer();

        if(enemyToPlayerDistance <= 10 && enemyToPlayerAngle <= 45)
        {
            //enemy sighted
            Debug.Log("ENEMY SIGHTED!");
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
        Debug.Log("Enemy to Player Distance - " + enemyToPlayerDistance);

        return Math.Sqrt((enemyToPlayerVector.x * enemyToPlayerVector.x) + (enemyToPlayerVector.y * enemyToPlayerVector.y) + (enemyToPlayerVector.z * enemyToPlayerVector.z));
    }

    double getAngleToPlayer()
    {
        //enemyToPlayerVector(dotProduct) / (enemyVectorLength * playerVectorLength) = COS θ

        //enemyVectorLength = Math.Sqrt((transform.position.x * transform.position.x) + (transform.position.y * transform.position.x) + (transform.position.z * transform.position.x));
        //playerVectorLength = Math.Sqrt((playerGO.transform.position.x * playerGO.transform.position.x) + (playerGO.transform.position.y * playerGO.transform.position.x) + (playerGO.transform.position.z * playerGO.transform.position.x));

        //enemyToPlayerAngle = Math.Acos(getVectorDotProduct() / (enemyVectorLength * playerVectorLength));

        //enemyToPlayerAngleInDegrees = enemyToPlayerAngle * 180 / Math.PI;

        Debug.Log("Enemy to Player Angle - " + Vector3.Angle(enemyToPlayerVector, transform.forward));

        return Vector3.Angle(enemyToPlayerVector, transform.forward);
    }
}
