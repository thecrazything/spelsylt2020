using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Expedition;

public class InventorySlot : MonoBehaviour
{
    public Sprite icon;

    InventoryItem item;

    public void AddItem(InventoryItem item)
    {
        this.item = item;
        icon = item.icon;
        //icon.enabled = true;
    }

    public void RemoveItem()
    {
        item = null;
        icon = null;
        //icon.enabled = false;
    }
}
