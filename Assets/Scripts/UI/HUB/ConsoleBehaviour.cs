using System;
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

    private TextPrintAnimation _textPrintAnimation;

    private string _startText = "";

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Text consoleTextView = GetComponent<Text>();
        if (consoleTextView == null)
        {
            throw new ArgumentNullException("No Text component exists on the GameObject");
        }

        _textPrintAnimation = new TextPrintAnimation(consoleTextView, writeDelay);
        _textPrintAnimation.Write(_startText);
        if (_audioSource != null)
        {
            StartCoroutine(SoundQueue.playNext(_audioSource, idleSound, 0.7f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_textPrintAnimation != null)
        {
            _textPrintAnimation.printNextIfTime(Time.deltaTime);
        }
    }

    public void WriteText(string text)
    {
        WriteText(text, false);
    }

    public void WriteTextWithSound(string text)
    {
        WriteText(text, true);
    }
    private void WriteText(string text, bool sound)
    {
        if (sound)
        {
            _audioSource.clip = writeSounds[UnityEngine.Random.Range(0, 2)];
            _audioSource.Play();
            StartCoroutine(SoundQueue.playNext(_audioSource, idleSound, 0.1f));
        }

        if (_textPrintAnimation != null)
        {
            _textPrintAnimation.Write(text);
        } else
        {
            _startText = text;
        }
    }
}
