using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PowerCell : MonoBehaviour
{
    public Light2D powerLight;

    private AudioSource _audio;

    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStatsService.Instance.gameStats.isPowerOn && !powerLight.enabled)
        {
            powerLight.enabled = true;
            _audio.Play();
        } 
        else if (!GameStatsService.Instance.gameStats.isPowerOn)
        {
            powerLight.enabled = false;
        }
    }
}
