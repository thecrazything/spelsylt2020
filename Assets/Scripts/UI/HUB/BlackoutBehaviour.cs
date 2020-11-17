using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutBehaviour : MonoBehaviour
{
    public delegate void BlackedOut(bool faded);
    public event BlackedOut onFadeFinished;

    public float fadeDelay = 1;
    CanvasGroup _panel;
    float _timeout;

    public bool fade;
    private bool _fading;
    private bool _black;

    // Start is called before the first frame update
    void Start()
    {
        _panel = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        _black = _panel.alpha == 0;

        if (fade && !_black)
        {
            _fading = true;
        }
        if (!fade && _black)
        {
            _fading = true;
        }

        if (fade && _fading) 
        {
            _panel.alpha -= (Time.deltaTime / fadeDelay);
            if (_panel.alpha == 0)
            {
                _fading = false;
                onFadeFinished?.Invoke(fade);
            }
        }

        if (!fade && _fading)
        {
            _panel.alpha += (Time.deltaTime / fadeDelay);
            if (_panel.alpha == 1)
            {
                _fading = false;
                onFadeFinished?.Invoke(fade);
            }
        }
    }

    public void SetFade(bool value)
    {
        fade = value;
    }

    public void FadeOut()
    {
        _panel.alpha = 0;
        fade = false;
    }

    public void FadeIn()
    {
        _panel.alpha = 1;
        fade = true;
    }

    public void SetBlack()
    {
        if (_panel != null)
        {
            _panel.alpha = 1;
        }
    }
}
