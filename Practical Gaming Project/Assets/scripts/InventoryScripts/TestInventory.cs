using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour {

    public Inventory myInventory;
	public ItemController ic;
	public GameObject cell;
	public GameObject keycardGameObject;

    public Keycard k1, k2, k3, k4;
    public Grenade g;

	// Use this for initialization
	void Start () {

        myInventory = GetComponent<Inventory>();
		ic = GetComponent<ItemController>();

        k1 = new Keycard("Keycard 1");
		k1.setName ("Keycard 1");
        k2 = new Keycard("Keycard 2");
		k2.setName ("Keycard 2");
        k3 = new Keycard("Keycard 3");
		k3.setName ("Keycard 3");
        k4 = new Keycard("Keycard 4");
		k4.setName ("Keycard 4");
        g = new Grenade("Stun Grenade");
        
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
			cell = GameObject.FindGameObjectWithTag("cell1");
			cell.SetActive (false);
			ic.makeItemActive (k2);

        }

        else if(gameObject.name == "Keycard2")
        {
            myInventory.addTo(k2);
            //Debug.Log("k2 added");
			cell = GameObject.FindGameObjectWithTag("cell3");
			cell.SetActive (false);
			ic.makeItemActive (k3);
        }

        else if (gameObject.name == "Keycard3")
        {
            myInventory.addTo(k3);
            //Debug.Log("k3 added");
			cell = GameObject.FindGameObjectWithTag("cell2");
			cell.SetActive (false);
			ic.makeItemActive (k4);
        }

        else if (gameObject.name == "Keycard4")
        {
            myInventory.addTo(k4);
            //Debug.Log("k4 added");
			cell = GameObject.FindGameObjectWithTag("cell5");
			cell.SetActive (false);
        }

        else if (gameObject.name == "StunGrenade")
        {
            myInventory.addTo(g);
            //Debug.Log("g1 added");
        }
    }
}
