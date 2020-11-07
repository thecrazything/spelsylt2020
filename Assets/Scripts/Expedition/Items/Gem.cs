using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Expedition;

public class Gem : MonoBehaviour, IInteractable
{
    public InventoryItem inventoryItem;

    public void Interact(Player player)
    {
        player.inventory.AddItem(inventoryItem);
        Debug.Log("Picked up a gem!");
        Destroy(gameObject);
    }
}
