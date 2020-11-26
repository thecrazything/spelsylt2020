using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsBehaviour : MonoBehaviour
{

    public AudioClip[] sounds;
    public AudioSource audioSourceL;
    public AudioSource audioSourceR;

    private PlayerMovement _movement;

    private float stepDelay = 0.5f;
    private float stepDelayTimer = 0f;
    private bool canPlaySound = true;
    private AudioSource _prevSource = null;

    // Start is called before the first frame update
    void Start()
    {
        _movement = GetComponentInParent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        float magnitude = _movement.GetMovement().magnitude;

        float modifier = _movement.isRunning() ? 0.5f : 1f;

        if(!canPlaySound)
        {
            if (stepDelayTimer >= (stepDelay * modifier))
            {
                stepDelayTimer = 0;
                canPlaySound = true;
            }
            else
            {
                stepDelayTimer += Time.deltaTime;
            }
        }

        if(magnitude > 0)
        {
            if(canPlaySound)
            {
                var source = GetNextSource();
                source.clip = GetRandomSound();
                source.Play();
                canPlaySound = false;
            }
        }
    }

    AudioClip GetRandomSound()
    {
        return sounds[Random.Range(0, sounds.Length)];
    }

    AudioSource GetNextSource()
    {
        if (_prevSource == audioSourceL)
        {
            _prevSource = audioSourceR;
            return audioSourceR;
        }
        _prevSource = audioSourceL;
        return audioSourceL;
    }
}
