using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventorySlot : MonoBehaviour
{
    public Transform title;
    Item _item;

    public event EventHandler<OnDeleteItemEventArgs> OnDeleteItem;
    public class OnDeleteItemEventArgs : EventArgs {
        public GameObject item;
    }

    public Item item
    {
        get { return _item; }
    }

    public void Delete(string test)
    {
        Debug.Log(test);
        Debug.Log("Delete item " + gameObject.name);
        OnDeleteItem?.Invoke(this, new OnDeleteItemEventArgs { item = gameObject });
    }

    public void Set(Item item)
    {
        _item = item;
        title.GetComponent<TextMeshProUGUI>().text = _item.GetName();
    }
}
