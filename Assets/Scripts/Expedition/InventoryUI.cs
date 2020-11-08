using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class InventoryUI : MonoBehaviour
{
    public Transform title;
    public Transform itemsParent;
    public GameObject wrapper;
    public string inventoryName;
    public Inventory inventory;

    InventorySlot[] slots;
    TextMeshProUGUI titleText;

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        titleText = title.GetComponent<TextMeshProUGUI>();
        inventoryName = transform.parent.name;

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
