using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item{

    private string name;
    private string type;

    public Item()
    {
        setType("Unknown");
        setName("Unknown");
    }

    public Item(string name, string type)
    {
        setType(type);
        setName(name);
    }

    public string getName()
    {
        return name;
    }

    public string getType()
    {
        return type;
    }

    public void setType(string type)
    {
        this.type = type;
    }

    public void setName(string name)
    {
        this.name = name;
    }

}
