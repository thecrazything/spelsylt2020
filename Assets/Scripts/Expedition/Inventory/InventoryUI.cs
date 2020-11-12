using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.UIElements;

public abstract class InventoryUI : MonoBehaviour
{
    public Transform title;
    public Transform scrollViewTransform;
    public GameObject wrapper;
    public string inventoryName;

    public IInventoryUiSource source;

    TextMeshProUGUI titleText;

    void Start()
    {
        titleText = title.GetComponent<TextMeshProUGUI>();
        inventoryName = transform.parent.name;

        Hide();
    }

    void UpdateUI()
    {
        var inventory = source.GetItems();

        for (int i = 0; i < inventory.Length; i++)
        {
            var item = inventory[i];
            if (item != null) {
                //slots[i].AddItem(item);
            }
        }
    }

    public void SetTitle(string name) {
        inventoryName = name;
    }

    public void Show()
    {
        titleText.text = name;
        UpdateUI();
        wrapper.SetActive(true);
    }

    public void Hide()
    {
        wrapper.SetActive(false);
    }

    public abstract void HandleSlot(InventorySlot slot);

    public void Toggle()
    {
        if (wrapper.activeSelf) {
            Hide();
        } else {
            Show();
        }
    }
}
