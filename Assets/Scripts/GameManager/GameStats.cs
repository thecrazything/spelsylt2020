using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{
    public delegate void GameStatChangeDelegate(GameStats stats);
    public event GameStatChangeDelegate onGameChange;

    public bool expeditionComplete = false;

    public HubTask[] hubTasks;

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
}
