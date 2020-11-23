using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapKey : Item 
{ 
    public string mapName;

    public MapKey(string map) 
    {
        mapName = map;
    }

    public override string GetName()
    {
        return mapName + " keycard";
    }
}
