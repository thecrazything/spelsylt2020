﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class LootContainer : MonoBehaviour, IInteractable, IInventoryUiSource
{
    public Sprite openSprite;
    public Sprite closeSpite;
    public AudioClip startSearchSound;

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

        source.GetComponent<Player>().interactor.OnLostInteractFocus += Interactor_OnLostInteractFocus;
        UpdateSprite();
        ui.Show();
    }

    public string GetId()
    {
        return (transform.position + name).ToString().Replace(" ", "");
    }

    private void Interactor_OnLostInteractFocus(object sender, Interactor.OnPlayerLostInteractFocus e)
    {
        ui.Hide();
        (sender as Interactor).OnLostInteractFocus -= Interactor_OnLostInteractFocus;
    }

    private void UpdateSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;
    }

    public float? GetActionTime(GameObject source)
    {
        if (isSearched) return null;
        else return 3;
    }

    public List<Item> GetInventory()
    {
        return inventory;
    }

    public string GetActionTitle(GameObject source)
    {
        return "Searching...";
    }

    public AudioClip GetActionSound(GameObject source)
    {
        return isSearched ? null : startSearchSound;
    }
}
