using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LootInjector : MonoBehaviour
{
    LootContainer container;

    public void InjectLoot()
    {
        if (!container)
        {
            container = GetComponent<LootContainer>();
        }
        container.inventory.AddRange(GetItems());
    }

    protected abstract Item[] GetItems();
}
