using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.UIElements;

public abstract class InventoryUI : MonoBehaviour
{
    public Transform title;
    public GameObject wrapper;
    public IInventoryUiSource source;

    TextMeshProUGUI titleText;

    public Transform list;
    public GameObject listItem;

    private string inventoryName;
    private bool isVisible = false;

    void Start()
    {
        titleText = title.GetComponent<TextMeshProUGUI>();

        Hide();
    }

    void UpdateUI()
    {
        EmptyList();

        var inventory = source.GetInventory().ToArray();

        for (int i = 0; i < inventory.Length; i++)
        {
            var item = inventory[i];
            if (item != null) {
                AddToList(item);
            }
        }
    }

    public void SetTitle(string name) {
        inventoryName = name;
    }

    public void Show()
    {
        if (isVisible) return;

        titleText.text = inventoryName;
        UpdateUI();
        wrapper.SetActive(true);

        isVisible = true;
    }

    public void Hide()
    {
        wrapper.SetActive(false);

        isVisible = false;
    }

    public void Toggle()
    {
        if (wrapper.activeSelf) {
            Hide();
        } else {
            Show();
        }
    }

    protected void EmptyList()
    {
        foreach (Transform child in list.transform)
        {
            Destroy(child.gameObject);
        }
    }

    protected void AddToList(Item item)
    {
        var i = Instantiate(listItem, list);
        InventorySlot slot = i.GetComponent<InventorySlot>();

        slot.Set(item);
        slot.OnClickItem += Slot_OnClickItem;
    }

    protected abstract void Slot_OnClickItem(object sender, InventorySlot.OnItemClickedEventArgs e);
}
