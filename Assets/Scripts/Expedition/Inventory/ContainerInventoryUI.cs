using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInventoryUI : InventoryUI
{
    public override void HandleSlot(InventorySlot slot)
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PlayerInventory playerInventory = player.inventory;

        Item item = slot.item;
        if (item != null)
        {
            playerInventory.items.Add(item);
            //source.inventory.Remove(item);
        }
        else
        {
            Debug.Log("Slot is empty");
        }
    }
}
