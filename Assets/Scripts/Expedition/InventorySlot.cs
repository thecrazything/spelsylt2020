using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Expedition;

public class InventorySlot : MonoBehaviour
{
    public Transform iconHolder;
    InventoryItem _item;

    public InventoryItem item {
        get { return _item; }
    }

    public void AddItem(InventoryItem item)
    {
        Image image = iconHolder.GetComponent<Image>();

        _item = item;
        image.sprite = item.icon;
        image.enabled = true;
    }

    public void RemoveItem()
    {
        Image image = iconHolder.GetComponent<Image>();

        _item = null;
        image.sprite = null;
        image.enabled = false;
    }
}
