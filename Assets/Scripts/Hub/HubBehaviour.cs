using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            GameStatsService.Instance.gameStats.newDay();
            consoleBehaviour.WriteText("Welcome back from expedition or whatever lol");
        }
        GameStatsService.Instance.gameStats.expeditionComplete = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            GameStatsService.Instance.selectedCharacter = null;
        }
    }
}
