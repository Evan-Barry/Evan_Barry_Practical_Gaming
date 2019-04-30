using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour {

    public float alertTime = 5;
    public bool timerRunning = false;

	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {
	    
        if(!timerRunning)
        {
            alertTime = 5f;
        }

        if(alertTime <= 0)
        {
            alertTime = 0;
        }
    }

    public void alertCountdown()
    {
        alertTime -= Time.deltaTime;
    }
}
