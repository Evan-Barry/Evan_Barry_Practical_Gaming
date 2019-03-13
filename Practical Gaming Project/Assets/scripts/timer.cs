using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour {

    public float alertTime = 10f;
    public bool timerRunning = false;
    public float cautionTime = 20f;

	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {
	    
        if(!timerRunning)
        {
            alertTime = 1f;
            cautionTime = 2f;
        }

        if(alertTime <= 0)
        {
            alertTime = 0;
        }

        if (cautionTime <= 0)
        {
            cautionTime = 0;
        }
    }

    public void alertCountdown()
    {
        alertTime -= Time.deltaTime;
    }

    public void cautionCountdown()
    {
        cautionTime -= Time.deltaTime;
    }
}
