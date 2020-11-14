﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class LootContainer : MonoBehaviour, IInteractable, IInventoryUiSource
{
    public GameObject inventoryUiPrefab;

    InventoryUI ui;
    public List<Item> inventory = new List<Item>();

    void Start()
    {
        ui = Instantiate(inventoryUiPrefab, transform).GetComponent<InventoryUI>();
        Debug.Log("UI: " + ui);
        ui.source = this;
    }

    public Item[] GetItems()
    {
        return inventory.ToArray();
    }

    public void Interact(GameObject source)
    {
        ui.Show();
    }

    public float? GetActionTime()
    {
        return null;
    }
}
