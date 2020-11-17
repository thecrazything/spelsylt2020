using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameStats
{
    public bool intro = true;
    public bool expeditionComplete = false;

    public HubTask[] hubTasks = { };
    private List<Item> _items = new List<Item>();

    public int daysLeft 
    {
        get
        {
            return _daysLeft;
        }
    }

    private int _daysLeft = 12;

    public void newDay()
    {
        _daysLeft -= 1;
    }

    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public void AddItems(Item[] items)
    {
        _items.AddRange(items);
    }

    public int RationCount()
    {
        return _items.Where(i => i is Ration).Count();
    }

    public void RemoveRation()
    {
        RemoveRation(1);
    }

    public void RemoveRation(int amount)
    {
        _items.Remove(_items.First(i => i is Ration));
    }
}
