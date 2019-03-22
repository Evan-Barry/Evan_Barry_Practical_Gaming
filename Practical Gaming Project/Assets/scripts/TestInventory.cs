using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour {

    public Inventory myInventory;

    Keycard a, b;
    Grenade c, d;

	// Use this for initialization
	void Start () {

        myInventory = GetComponent<Inventory>();

        a = new Keycard("Door 1");
        b = new Keycard("Door 2");
        c = new Grenade("Stun");
        d = new Grenade("Chaff");
        
        myInventory.addTo(a);
        myInventory.addTo(b);
        myInventory.addTo(c);
        myInventory.addTo(d);

        Item randItem = myInventory.getItem(Random.Range(0, 4));

        if (randItem is Keycard)
        {
            (randItem as Keycard).snapKeycard();
        }

        if (randItem is Grenade)
        {
            (randItem as Grenade).boomGrenade();
        }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
