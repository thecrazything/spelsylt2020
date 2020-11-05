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

    private string _startText = TextConstants.INTRO_MESSAGE;

    // Start is called before the first frame update
    void Start()
    {

        _audioSource = GetComponent<AudioSource>();
        Text consoleTextView = GetComponent<Text>();

        if (_audioSource == null)
        {
            throw new ArgumentNullException("No AudioSource component view exists on the GameObject");
        }
        if (consoleTextView == null)
        {
            throw new ArgumentNullException("No Text component exists on the GameObject");
        }

        _textPrintAnimation = new TextPrintAnimation(consoleTextView, writeDelay);
        _textPrintAnimation.Write(_startText);
        GameStatsService.Instance.onChangeSelectedCharacter += character =>
        {
            if (character == null)
            {
                _textPrintAnimation.Write(TextConstants.IDLE_TEXT);
            } 
            else
            {
                string txt = TextConstants.USER_DETAIL_NAME_TEXT + "\n" + TextConstants.USER_HEALTH_5_TEXT; // TODO switch based on health
                _textPrintAnimation.Write(txt.Replace("{name}", character.name));
                _audioSource.clip = writeSounds[UnityEngine.Random.Range(0, 2)];
                _audioSource.Play();
                StartCoroutine(SoundQueue.playNext(_audioSource, idleSound, 0.1f));
            }
        };
        StartCoroutine(SoundQueue.playNext(_audioSource, idleSound, 0.7f));
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
        if (_textPrintAnimation != null)
        {
            _textPrintAnimation.Write(text);
        } else
        {
            _startText = text;
        }
    }
}
