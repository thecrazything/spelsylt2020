using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ContainerInventoryUI : InventoryUI
{
    protected override void Slot_OnClickItem(object sender, InventorySlot.OnItemClickedEventArgs e)
    {
        source.GetInventory().Remove(e.slot.item);

        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().inventory.items.Add(e.slot.item);
        Destroy(e.gameObject);
    }
}
