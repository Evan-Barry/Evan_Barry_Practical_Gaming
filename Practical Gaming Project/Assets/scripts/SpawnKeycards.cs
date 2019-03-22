using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKeycards : MonoBehaviour {

    public GameObject keycard;
    public GameObject spotlight;
    GameObject kc1, kc2, kc3, kc4;
    GameObject sl1, sl2, sl3, sl4;

	// Use this for initialization
	void Start () {

        kc1 = Instantiate(keycard, new Vector3(-40, 1, 6.5f), transform.rotation);
        kc2 = Instantiate(keycard, new Vector3(-34, 1, -3), transform.rotation);
        kc3 = Instantiate(keycard, new Vector3(-9, 1, -3), transform.rotation);
        kc4 = Instantiate(keycard, new Vector3(37.5f, 1, 13), transform.rotation);

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

        kc1.transform.Rotate(0, 1, 0);
        kc2.transform.Rotate(0, 1, 0);
        kc3.transform.Rotate(0, 1, 0);
        kc4.transform.Rotate(0, 1, 0);
    }
}
