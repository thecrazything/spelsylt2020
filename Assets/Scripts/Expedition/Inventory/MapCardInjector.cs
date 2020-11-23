using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCardInjector : LootInjector
{
    public string mapName;

    protected override Item[] GetItems()
    {
        return new Item[] { new MapKey(mapName) };
    }
}
