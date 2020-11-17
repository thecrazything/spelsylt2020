using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{
    public bool intro = true;
    public bool expeditionComplete = false;

    public HubTask[] hubTasks = { };
    private Ration[] _rations = { };

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

    public void AddRation(Ration ration)
    {
        var temp = new Ration[_rations.Length + 1];
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
        var temp = new Ration[_rations.Length - amount];
        for (var i = 0; i < temp.Length; i++)
        {
            temp[i] = _rations[i];
        }
        _rations = temp;
    }
}
