using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryUI : InventoryUI
{
    protected override void Slot_OnClickItem(object sender, InventorySlot.OnItemClickedEventArgs e)
    {
        source.GetInventory().Remove(e.slot.item);

        Destroy(e.gameObject);
    }
}
