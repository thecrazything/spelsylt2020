using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats
{
    public delegate void GameStatChangeDelegate(GameStats stats);
    public event GameStatChangeDelegate onGameChange;

    public bool expeditionComplete = false;
}
