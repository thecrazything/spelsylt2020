﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaysLeftBehaviour : MonoBehaviour
{

    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
        _text.text = TextConstants.DAYS_LEFT.Replace("{days}", GameStatsService.Instance.gameStats.daysLeft.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = TextConstants.DAYS_LEFT.Replace("{days}", GameStatsService.Instance.gameStats.daysLeft.ToString());
    }
}
