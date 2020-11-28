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
        if (GameStatsService.Instance.gameStats.isPowerOn)
        {
            powerLight.enabled = true;
            if (!_audio.isPlaying)
            {
                _audio.Play();
            }
        } 
        else
        {
            powerLight.enabled = false;
        }
    }
}
