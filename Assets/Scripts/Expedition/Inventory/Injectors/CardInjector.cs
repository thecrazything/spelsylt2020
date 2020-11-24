using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInjector : LootInjector
{
    public KeycardColor color;

    protected override Item[] GetItems()
    {
        return new Item[] { new Keycard(color) };
    }
}
