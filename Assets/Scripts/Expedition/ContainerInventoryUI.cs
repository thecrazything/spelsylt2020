using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Expedition;

public class ContainerInventoryUI : InventoryUI
{
    public override void HandleSlot(InventorySlot slot)
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Inventory playerInventory = player.inventory;

        InventoryItem item = slot.item;
        if (item != null)
        {
            playerInventory.AddItem(item);
            inventory.RemoveItem(item);
            slot.Clear();
        }
        else
        {
            Debug.Log("Slot is empty");
        }
    }
}
