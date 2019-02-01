using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

    Vector3 enemyToPlayerDirection;
    Transform playerTransform;
    Vector3 playerTransformNormalised;
    GameObject playerGO;

	// Use this for initialization
	void Start () {
        playerGO = GameObject.FindGameObjectWithTag("Player");
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
            transform.Rotate(Vector3.up * -20 * Time.deltaTime); 
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * 20 * Time.deltaTime);
        }

        Debug.Log("Enemy Forward" + transform.forward);

        playerTransform = playerGO.GetComponent<Transform>();

        playerTransformNormalised = Vector3.Normalize(playerTransform.position);

        enemyToPlayerDirection = new Vector3((transform.position.x - playerTransformNormalised.x), (transform.position.y - playerTransformNormalised.y), (transform.position.z - playerTransformNormalised.z));
        Debug.Log("Enemy to Player Direction" + enemyToPlayerDirection);
    }
}
