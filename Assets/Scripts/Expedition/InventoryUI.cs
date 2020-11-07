using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject wrapper;

    Inventory playerInventory;
    InventorySlot[] slots;

    void Start()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerInventory = player.inventory;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        Hide();
    }

    void UpdateUI()
    {
        Debug.Log("Updating UI");
        for (int i = 0; i < playerInventory.size; i++)
        {
            var item = playerInventory.GetItem(i);
            if (item != null) {
                slots[i].AddItem(item);
            }
        }
    }

    public void Show()
    {
        UpdateUI();
        wrapper.SetActive(true);
    }

    public void Hide()
    {
        wrapper.SetActive(false);
    }

    public void Toggle()
    {
        if (wrapper.active) {
            Hide();
        } else {
            Show();
        }
    }
}
