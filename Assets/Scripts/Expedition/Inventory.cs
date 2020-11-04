using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Expedition;

public class Inventory
{
    int _size;
    InventoryItem[] _inventory;

    public Inventory(int size)
    {
        _size = size;
        _inventory = new InventoryItem[_size];
    }

    public bool AddItem(InventoryItem item)
    {
        for (int i = 0; i < _size; i++)
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

    public InventoryItem GetItem(int i)
    {
        return _inventory[i];
    }
}
