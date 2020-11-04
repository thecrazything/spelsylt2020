using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubBehaviour : MonoBehaviour
{
    public ConsoleBehaviour consoleBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        if (GameStatsService.Instance.gameStats == null)
        {
            throw new ArgumentNullException("No Gamestats found in GameStatsService");
        }
        if (GameStatsService.Instance.gameStats.expeditionComplete)
        {
            consoleBehaviour.WriteText("Welcome back from expedition or whatever lol");
        }
        GameStatsService.Instance.gameStats.expeditionComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
