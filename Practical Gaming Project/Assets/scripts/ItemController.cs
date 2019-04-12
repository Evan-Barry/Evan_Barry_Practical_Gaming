using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {

    public GameObject keycard;
    public GameObject spotlight;
    public GameObject grenade;
    GameObject kc1, kc2, kc3, kc4;
    GameObject sl1, sl2, sl3, sl4, sl5, sl6;
    GameObject g1, g2;

    // Use this for initialization
    void Start () {

        kc1 = Instantiate(keycard, new Vector3(-40, 1, 6.5f), transform.rotation);
        kc1.name = "Keycard1";
        kc2 = Instantiate(keycard, new Vector3(-34, 1, -3), transform.rotation);
        kc2.name = "Keycard2";
        kc3 = Instantiate(keycard, new Vector3(-9, 1, -3), transform.rotation);
        kc3.name = "Keycard3";
        kc4 = Instantiate(keycard, new Vector3(37.5f, 1, 13), transform.rotation);
        kc4.name = "Keycard4";

        sl1 = Instantiate(spotlight, new Vector3(0, 3, 0), Quaternion.Euler(new Vector3 (90, 0, 0)));
        sl1.transform.SetParent(kc1.transform, false);
        sl2 = Instantiate(spotlight, new Vector3(0, 3, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
        sl2.transform.SetParent(kc2.transform, false);
        sl3 = Instantiate(spotlight, new Vector3(0, 3, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
        sl3.transform.SetParent(kc3.transform, false);
        sl4 = Instantiate(spotlight, new Vector3(0, 3, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
        sl4.transform.SetParent(kc4.transform, false);
    }
	
	// Update is called once per frame
	void Update () {
        
        //if(kc1 != null)
        //{
        //    kc1.transform.Rotate(0, 1, 0);
        //}
        
        //if(kc2 != null)
        //{
        //    kc2.transform.Rotate(0, 1, 0);
        //}
        
        //if(kc3 != null)
        //{
        //    kc3.transform.Rotate(0, 1, 0);
        //}
        
        //if(kc4 != null)
        //{
        //    kc4.transform.Rotate(0, 1, 0);
        //}
    }

    
}
