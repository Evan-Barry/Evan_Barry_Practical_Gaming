using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour {

    public Inventory myInventory;

    public Keycard k1, k2, k3, k4;
    public Grenade g1, g2;

	// Use this for initialization
	void Start () {

        myInventory = GetComponent<Inventory>();

        k1 = new Keycard("Keycard 1");
        k2 = new Keycard("Keycard 2");
        k3 = new Keycard("Keycard 3");
        k4 = new Keycard("Keycard 4");
        g1 = new Grenade("Stun Grenade");
        g2 = new Grenade("Stun Grenade");
        
	}
	
	// Update is called once per frame
	void Update () {

    }
}
