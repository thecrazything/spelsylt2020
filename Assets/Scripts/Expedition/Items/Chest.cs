using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Expedition;

public class Chest : MonoBehaviour, IInteractable
{
    public GameObject inventoryUiPrefab;
    public InventoryItem[] items;

    ContainerInventoryUI ui;
    Inventory inventory;

    void Start() {
        inventory = new Inventory(items.Length);
        ui = Instantiate(inventoryUiPrefab, transform).GetComponent<ContainerInventoryUI>();

        foreach (InventoryItem item in items)
        {
            inventory.AddItem(item);
        }

        ui.inventory = inventory;
    }

    public void Interact(Player player)
    {
        ui.Show();
    }
}
