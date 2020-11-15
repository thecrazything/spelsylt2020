using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class LootContainer : MonoBehaviour, IInteractable, IInventoryUiSource
{
    public Sprite openSprite;
    public Sprite closeSpite;

    public string ContainerName;

    private bool isOpen = false;
    private bool isSearched = false;

    public GameObject inventoryUiPrefab;

    InventoryUI ui;
    public List<Item> inventory = new List<Item>();

    void Start()
    {
        ui = Instantiate(inventoryUiPrefab, transform).GetComponent<InventoryUI>();
        ui.source = this;
        ui.SetTitle(ContainerName);
    }

    public void Interact(GameObject source)
    {
        isSearched = true;
        isOpen = true;

        UpdateSprite();
        ui.Show();
    }

    private void UpdateSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;
    }

    public float? GetActionTime()
    {
        if (isSearched) return null;
        else return 3;
    }

    public List<Item> GetInventory()
    {
        return inventory;
    }

    public string GetActionTitle()
    {
        return "Searching...";
    }
}
