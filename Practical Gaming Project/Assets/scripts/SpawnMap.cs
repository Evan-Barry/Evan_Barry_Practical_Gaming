using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour {

    public GameObject map;

	// Use this for initialization
	void Start () {

        Instantiate(map);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
