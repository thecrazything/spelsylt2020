using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ration : Item
{
    public Ration()
    {
        name = "Ration";
    }

    public override string GetName()
    {
        return name;
    }
}
