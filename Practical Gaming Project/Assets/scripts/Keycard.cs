using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : Item {

	public Keycard(string name): base(name, "Keycard")
    {

    }

    public void snapKeycard()
    {
        Debug.Log("Snap");
    }
}
