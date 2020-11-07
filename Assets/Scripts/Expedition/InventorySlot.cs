using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Expedition;

public class InventorySlot : MonoBehaviour
{
    public Transform iconHolder;

    InventoryItem item;
    Image image;

    void Start() {
        image = iconHolder.GetComponent<Image>();
    }

    public void AddItem(InventoryItem item)
    {
        this.item = item;
        image.sprite = item.icon;
        image.enabled = true;
    }

    public void RemoveItem()
    {
        item = null;
        image.sprite = null;
        image.enabled = false;
    }
}
