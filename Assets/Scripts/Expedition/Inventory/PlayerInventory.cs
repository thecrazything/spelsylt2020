using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : IInventoryUiSource
{
    public List<Item> items;

    public PlayerInventory() {
        items = new List<Item>();
    }

    public Item[] GetItems()
    {
        return items.ToArray();
    }
}
