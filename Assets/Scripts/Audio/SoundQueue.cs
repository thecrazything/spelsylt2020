using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundQueue
{
    public static IEnumerator playNext(AudioSource audioSource, AudioClip audio, float delayAdjustment)
    {
        yield return new WaitForSeconds(audioSource.clip.length - delayAdjustment);
        audioSource.clip = audio;
        audioSource.Play();
    }
}
