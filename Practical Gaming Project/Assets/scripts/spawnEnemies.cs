using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEnemies : MonoBehaviour {
    
    Renderer rend;
    public GameObject enemy;

	// Use this for initialization
	void Start () {

        rend = GetComponent<Renderer>();

        Debug.Log("Plane size - " + rend.bounds.size);
        
        Instantiate(enemy, new Vector3(23, 0.5f, 23), Quaternion.Euler(new Vector3(0,-90,0)));

        Instantiate(enemy, new Vector3(23, 0.5f, -23), transform.rotation);
        
        Instantiate(enemy, new Vector3(-23, 0.5f, 23), Quaternion.Euler(new Vector3(0, 180, 0)));

        Instantiate(enemy, new Vector3(-23, 0.5f, -23), Quaternion.Euler(new Vector3(0, 90, 0)));
    }
	
	// Update is called once per frame
	void Update () {

	}
}
