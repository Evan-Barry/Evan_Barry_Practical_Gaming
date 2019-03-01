using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    List<Item> Items;

	// Use this for initialization
	void Start () {

        Items = new List<Item>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addTo(Item newItem)
    {
        Items.Add(newItem);
    }

    internal Item getItem(int v)
    {
        return Items[v];
    }
}
