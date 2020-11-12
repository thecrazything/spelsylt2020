using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Transform title;
    Item _item;

    public Item item
    {
        get { return _item; }
    }

    public void Set(Item item)
    {
        _item = item;
        title.GetComponent<TextMeshProUGUI>().text = _item.GetName();
    }
}
