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

    public void add(GameObject gameObject)
    {
        if (gameObject.name == "Keycard1")
        {
            myInventory.addTo(k1);
            //Debug.Log("k1 added");
        }

        else if(gameObject.name == "Keycard2")
        {
            myInventory.addTo(k2);
            //Debug.Log("k2 added");
        }

        else if (gameObject.name == "Keycard3")
        {
            myInventory.addTo(k3);
            //Debug.Log("k3 added");
        }

        else if (gameObject.name == "Keycard4")
        {
            myInventory.addTo(k4);
            //Debug.Log("k4 added");
        }

        else if (gameObject.name == "Grenade1")
        {
            myInventory.addTo(g1);
            //Debug.Log("g1 added");
        }

        else if (gameObject.name == "Grenade2")
        {
            myInventory.addTo(g2);
            //Debug.Log("g2 added");
        }
    }
}
