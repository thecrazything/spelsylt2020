using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Expedition;

public class Inventory
{
    public int size;
    InventoryItem[] _inventory;

    public Inventory(int size)
    {
        this.size = size;
        _inventory = new InventoryItem[size];
    }

    public bool AddItem(InventoryItem item)
    {
        for (int i = 0; i < _inventory.Length; i++)
        {
            if (_inventory[i] == null) {
                _inventory[i] = item;
                return true;
            }
        }

        return false;
    }

    public void EmptyIndex(int i)
    {
        _inventory[i] = null;
    }

    public InventoryItem[] GetAllItems() {
        return _inventory;
    }

    public InventoryItem GetItem(int i)
    {
        return _inventory[i];
    }
}
