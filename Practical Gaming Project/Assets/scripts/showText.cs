using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showText : MonoBehaviour {

    public Text text;
    private enum upDown {  Down = -1, Start = 0, Up = 1};
    private upDown textChanged = upDown.Start;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
}
