using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleBehaviour : MonoBehaviour
{
    public float writeDelay = 0.1f;
    public AudioClip startSound;
    public AudioClip idleSound;
    public AudioClip stopSound;
    public AudioClip[] writeSounds;

    private AudioSource _audioSource;

    private string defaultText = "Much need to be done in the HUB and on expeditions or whatever lol.";
    private string characterSelectedText = "{name} is feeling pretty swell. Pretty swiggity swooty. Hip with the kids.";

    private TextPrintAnimation _textPrintAnimation;


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _textPrintAnimation = new TextPrintAnimation(GetComponent<Text>(), writeDelay);
        _textPrintAnimation.Write(defaultText);
        GameStatsService.Instance.onChangeSelectedCharacter += character =>
        {
            if (character == null)
            {
                _textPrintAnimation.Write(defaultText);
            } 
            else
            {
                _textPrintAnimation.Write(characterSelectedText.Replace("{name}", character.name));
                _audioSource.clip = writeSounds[Random.Range(0, 2)];
                _audioSource.Play();
                StartCoroutine(SoundQueue.playNext(_audioSource, idleSound, 0.1f));
            }
        };
        StartCoroutine(SoundQueue.playNext(_audioSource, idleSound, 0.7f));
    }

    // Update is called once per frame
    void Update()
    {
        _textPrintAnimation.printNextIfTime(Time.deltaTime);
    }
}
