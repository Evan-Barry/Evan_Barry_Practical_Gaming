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
		
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(textChanged != upDown.Down)
            {
                text.text = "Text Changed";
                textChanged = upDown.Down;
            }

            else
            {
                text.text = "Text Changed Back";
                textChanged = upDown.Up;
            }
        }

	}

    private void Awake()
    {
        GameObject canvasGO = new GameObject();
        canvasGO.name = "Canvas";
        canvasGO.AddComponent<Canvas>();

        Canvas canvas;
        canvas = canvasGO.GetComponent<Canvas>();

        GameObject textGO = new GameObject();
        textGO.transform.parent = canvasGO.transform;
        textGO.AddComponent<Text>();
    }

    public void Text()
    {
        text.text = gameObject.name;
        StartCoroutine("WaitForSec");
    }

    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(2);
        text.text = "";
    }
}
