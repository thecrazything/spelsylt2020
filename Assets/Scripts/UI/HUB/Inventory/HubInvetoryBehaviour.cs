using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubInvetoryBehaviour : InventoryUI, IInventoryUiSource
{
    public HubPlayerInvetoryBehaviour playerInventory;
    public List<Item> hubInventory { get; private set; }
    public HubInvetoryBehaviour()
    {
        source = this;
    }
    public List<Item> GetInventory()
    {
        return hubInventory.FindAll(x => !(x is Ration));
    }

    protected override void Slot_OnClickItem(object sender, InventorySlot.OnItemClickedEventArgs e)
    {
        hubInventory.Remove(e.slot.item);
        playerInventory.AddItem(e.slot.item);
        UpdateUI();
    }

    protected override void OnStart()
    {
        hubInventory = new List<Item>(GameStatsService.Instance.gameStats.GetItems());
        UpdateUI();
    }

    public override void Show()
    {
        hubInventory = new List<Item>(GameStatsService.Instance.gameStats.GetItems());
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

    public void AddItem(Item item)
    {
        hubInventory.Add(item);
        UpdateUI();
    }

}
