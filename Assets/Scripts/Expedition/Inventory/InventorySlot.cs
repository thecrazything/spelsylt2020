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

    public event EventHandler<OnItemClickedEventArgs> OnClickItem;
    public class OnItemClickedEventArgs : EventArgs {
        public InventorySlot slot;
        public GameObject gameObject;
    }

    public Item item
    {
        get { return _item; }
    }

    public void ClickItem()
    {
        OnClickItem?.Invoke(this, new OnItemClickedEventArgs
        {
            slot = this,
            gameObject = gameObject
        }) ;
    }

    public void Set(Item item)
    {
        _item = item;
        title.GetComponent<TextMeshProUGUI>().text = _item.GetName();
    }
}
