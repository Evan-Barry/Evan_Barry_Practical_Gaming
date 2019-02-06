using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

    Vector3 enemyToPlayerVector;
    double enemyToPlayerDistance;
    Boolean enemyFacingPlayer = false;
    double enemyVectorLength;
    double playerVectorLength;
    double enemyToPlayerAngle;
    double enemyToPlayerAngleInDegrees;
    
    GameObject playerGO;
    GameObject spotLightGO;

	// Use this for initialization
	void Start () {
        playerGO = GameObject.FindGameObjectWithTag("Player");

        spotLightGO = GameObject.FindGameObjectWithTag("EnemySpotLight");
    }
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * -40 * Time.deltaTime); 
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * 40 * Time.deltaTime);
        }

        spotLightGO.transform.position = transform.position;
        spotLightGO.transform.rotation = transform.rotation;

        getEnemyPosition();

        getPlayerPosition();

        getEnemytoPlayerVector();

        getEnemyToPlayerDistance();

        getEnemyForward();

        enemyFacingPlayer = getEnemyFacingPlayer();

        isEnemyFacingPlayer();

        getAngleToPlayer();

        //getVectorDotProduct();
    }

    void getEnemyForward()
    {
        Debug.Log("Enemy Forward" + transform.forward);
    }

    void getEnemytoPlayerVector()
    {
        enemyToPlayerVector = new Vector3((playerGO.transform.position.x - transform.position.x), (playerGO.transform.position.y - transform.position.y), (playerGO.transform.position.z - transform.position.z));

        Debug.Log("Enemy to Player Vector" + enemyToPlayerVector);
    }

    void getEnemyPosition()
    {
        Debug.Log("Enemy Position - " + transform.position);
    }

    void getPlayerPosition()
    {
        Debug.Log("Player Position - " + playerGO.transform.position);
    }

    void getEnemyToPlayerDistance()
    {
        enemyToPlayerDistance = Math.Sqrt((enemyToPlayerVector.x * enemyToPlayerVector.x) + (enemyToPlayerVector.y * enemyToPlayerVector.y) + (enemyToPlayerVector.z * enemyToPlayerVector.z));

        Debug.Log("Enemy to Player Distance - " + enemyToPlayerDistance);
    }



    Boolean getEnemyFacingPlayer()
    {
        if(getVectorDotProduct() > 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    void isEnemyFacingPlayer()
    {
        if(enemyFacingPlayer)
        {
            Debug.Log("Enemy is facing player");
        }
        else
        {
            Debug.Log("Enemy is NOT facing player");
        }
    }

    void getAngleToPlayer()
    {
        //enemyToPlayerVector(dotProduct) / (enemyVectorLength * playerVectorLength) = COS θ

        enemyVectorLength = Math.Sqrt((transform.position.x * transform.position.x) + (transform.position.y * transform.position.x) + (transform.position.z * transform.position.x));
        playerVectorLength = Math.Sqrt((playerGO.transform.position.x * playerGO.transform.position.x) + (playerGO.transform.position.y * playerGO.transform.position.x) + (playerGO.transform.position.z * playerGO.transform.position.x));

        enemyToPlayerAngle = Math.Acos(getVectorDotProduct() / (enemyVectorLength * playerVectorLength));

        enemyToPlayerAngleInDegrees = enemyToPlayerAngle * 180 / Math.PI;

        Debug.Log("Enemy to Player Angle - " + enemyToPlayerAngleInDegrees);
    }

    double getVectorDotProduct()
    {
        return ((enemyToPlayerVector.x * transform.forward.x) + (enemyToPlayerVector.y * transform.forward.y) + (enemyToPlayerVector.z * transform.forward.z));
    }
}
