using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootInjector : MonoBehaviour
{
    public Item[] items;

    LootContainer container;
    void Awake()
    {
        container = GetComponent<LootContainer>();
    }

    public void InjectLoot()
    {
        container.inventory.AddRange(items);
    }
}
