using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PowerCell : MonoBehaviour
{
    public Light2D powerLight;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStatsService.Instance.gameStats.isPowerOn)
        {
            powerLight.enabled = true;
        } 
        else
        {
            powerLight.enabled = false;
        }
    }
}
