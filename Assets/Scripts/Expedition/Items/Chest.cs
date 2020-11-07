using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Expedition;

public class Chest : MonoBehaviour, IInteractable
{
    public GameObject inventoryUiPrefab;
    public InventoryItem[] items;

    InventoryUI ui;
    Inventory inventory;

    void Start() {
        inventory = new Inventory(items.Length);
        ui = Instantiate(inventoryUiPrefab, transform).GetComponent<InventoryUI>();

        foreach (InventoryItem item in items)
        {
            inventory.AddItem(item);
        }
    }

    public void Interact(Player player)
    {
        ui.Show();
        Debug.Log("Hello! I'm a chest");
    }
}
