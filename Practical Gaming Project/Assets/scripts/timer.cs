using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour {

    public float lookTime = 2;
    public bool timerRunning = false;

	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {
	    
        if(!timerRunning)
        {
            lookTime = 2f;
        }

        if(lookTime <= 0)
        {
            lookTime = 0;
        }
    }

    public void countdown()
    {
        lookTime -= Time.deltaTime;
    }
}
