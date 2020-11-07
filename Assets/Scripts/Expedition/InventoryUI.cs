using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.Expedition;

public class InventoryUI : MonoBehaviour
{
    public Transform title;
    public Transform itemsParent;
    public GameObject wrapper;
    public string name;
    public bool isPlayerInventory = false;
    public Inventory inventory;

    Inventory playerInventory;
    InventorySlot[] slots;
    TextMeshProUGUI titleText;

    void Start()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerInventory = player.inventory;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        titleText = title.GetComponent<TextMeshProUGUI>();
        name = transform.parent.name;

        foreach (InventorySlot slot in slots) {
            slot.GetComponentInChildren<Button>().onClick.AddListener(delegate { HandleSlot(slot); });
        }

        Hide();
    }

    void UpdateUI()
    {
        for (int i = 0; i < inventory.size; i++)
        {
            var item = inventory.GetItem(i);
            if (item != null) {
                slots[i].AddItem(item);
            }
        }
    }

    public void SetTitle(string name) {
        this.name = name;
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

    public void HandleSlot(InventorySlot slot)
    {
        if (isPlayerInventory) { return; }

        InventoryItem item = slot.item;
        if (item != null) {
            playerInventory.AddItem(item);
            slot.RemoveItem();
        } else {
            Debug.Log("Slot is empty");
        }
    }

    public void Toggle()
    {
        if (wrapper.activeSelf) {
            Hide();
        } else {
            Show();
        }
    }
}
