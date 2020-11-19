using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPlayerInvetoryBehaviour : InventoryUI, IInventoryUiSource
{
    public HubInvetoryBehaviour hubInventory;
    public List<Item> playerInventory { get; private set; }
    public HubPlayerInvetoryBehaviour()
    {
        source = this;
    }
    public List<Item> GetInventory()
    {
        return playerInventory;
    }

    public override void Show()
    {
        playerInventory = new List<Item>();
        if (isVisible) return;
        wrapper.SetActive(true);
        UpdateUI();
        isVisible = true;
    }

    public override void Hide()
    {
        wrapper.SetActive(false);

        isVisible = false;
    }

    protected override void Slot_OnClickItem(object sender, InventorySlot.OnItemClickedEventArgs e)
    {
        playerInventory.Remove(e.slot.item);
        hubInventory.AddItem(e.slot.item);
        UpdateUI();
    }

    protected override void OnStart()
    {
        playerInventory = new List<Item>();
        UpdateUI();
    }

    public void AddItem(Item item)
    {
        playerInventory.Add(item);
        UpdateUI();
    }

}