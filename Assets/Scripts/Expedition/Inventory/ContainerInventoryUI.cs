using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using System;

public class ContainerInventoryUI : InventoryUI
{
    public Transform list;
    public GameObject listItem;

    public void AddToList()
    {
        var i = Instantiate(listItem, list);
        InventorySlot slot = i.GetComponent<InventorySlot>();

        slot.Set(new Ration());
        slot.OnDeleteItem += Slot_OnDeleteItem;

        Debug.Log(i.GetComponent<RectTransform>().sizeDelta.x);
        Debug.Log(i.GetComponent<RectTransform>().sizeDelta.y);
    }

    private void Slot_OnDeleteItem(object sender, InventorySlot.OnDeleteItemEventArgs e)
    {
        Debug.Log("Recieved " + e.item.name);
        Destroy(e.item);
    }
}
