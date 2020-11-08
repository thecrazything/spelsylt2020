using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{
    public delegate void GameStatChangeDelegate(GameStats stats);
    public event GameStatChangeDelegate onGameChange;

    public bool expeditionComplete = false;

    public HubTask[] hubTasks = { };
    private RationItem[] _rations = { };

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

        onGameChange?.Invoke(this);
    }

    public void AddRation(RationItem ration)
    {
        var temp = new RationItem[_rations.Length + 1];
        _rations.CopyTo(temp, 0);
        temp[temp.Length - 1] = ration;
        _rations = temp;
    }

    public int RationCount()
    {
        return _rations.Length;
    }

    public void RemoveRation()
    {
        RemoveRation(1);
    }

    public void RemoveRation(int amount)
    {
        var temp = new RationItem[_rations.Length - amount];
        for (var i = 0; i < temp.Length; i++)
        {
            temp[i] = _rations[i];
        }
        _rations = temp;
    }
}
