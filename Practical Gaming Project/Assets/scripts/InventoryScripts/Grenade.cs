using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Item {
    
    public Grenade(string name) : base(name, "Grenade")
    {

    }

    public void boomGrenade()
    {
        Debug.Log("Boom");
    }
}
