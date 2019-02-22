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

        Instantiate(enemy, new Vector3(-25, 0.5f, -25), transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
