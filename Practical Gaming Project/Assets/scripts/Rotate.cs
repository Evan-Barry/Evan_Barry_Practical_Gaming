using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(gameObject.CompareTag("pickup"))
        {
            transform.Rotate(0, 1, 0);
        }
        else
        {
            transform.Rotate(0, 0, 0);
        }
        
    }
}
