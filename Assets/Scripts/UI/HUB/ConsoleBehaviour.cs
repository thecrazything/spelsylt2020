using System;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleBehaviour : MonoBehaviour
{
    public float writeDelay = 0.1f;
    public AudioClip startSound;
    public AudioClip idleSound;
    public AudioClip stopSound;
    public AudioClip shutdownSound;
    public AudioClip[] writeSounds;

    private AudioSource _audioSource;

    private TextPrintAnimation _textPrintAnimation;

    private string _startText = "";
    private Func<bool, bool> _startOnComplete;

    private Text _consoleTextView;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _consoleTextView = GetComponent<Text>();
        if (_consoleTextView == null)
        {
            throw new ArgumentNullException("No Text component exists on the GameObject");
        }

        _textPrintAnimation = new TextPrintAnimation(_consoleTextView, writeDelay);
        _textPrintAnimation.Write(_startText, _startOnComplete);
        _startOnComplete = null;
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

    public void Clear()
    {
        _textPrintAnimation.Clear();
    }

    public void ShutDown()
    {
        Clear();
        _audioSource.clip = shutdownSound;
        _audioSource.Play();
    }

    public void WriteText(string text)
    {
        WriteText(text, null);
    }


    public void WriteText(string text, Func<bool, bool> onComplete)
    {
        WriteText(text, false, onComplete);
    }

    public void WriteTextWithSound(string text)
    {
        WriteTextWithSound(text, null);
    }

    public void WriteTextWithSound(string text, Func<bool, bool> onComplete)
    {
        WriteText(text, true, onComplete);
    }

    public int GetRows()
    {
        return _consoleTextView.text.Split('\n').Length;
    }

    private void WriteText(string text, bool sound, Func<bool, bool> onComplete)
    {
        if (sound)
        {
            _audioSource.clip = writeSounds[UnityEngine.Random.Range(0, 2)];
            _audioSource.Play();
            StartCoroutine(SoundQueue.playNext(_audioSource, idleSound, 0.1f));
        }

        if (_textPrintAnimation != null)
        {
            _textPrintAnimation.Write(text, onComplete);
        } else
        {
            _startText = text;
            _startOnComplete = onComplete;
        }
    }
}
