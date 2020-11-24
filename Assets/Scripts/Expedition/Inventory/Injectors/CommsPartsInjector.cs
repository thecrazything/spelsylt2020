using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommsPartsInjector : LootInjector
{
    protected override Item[] GetItems()
    {
        return new Item[] { new CommsParts() };
    }
}
