using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackoutBehaviour : MonoBehaviour
{
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
            }
        }

        if (!fade && _fading)
        {
            _panel.alpha += (Time.deltaTime / fadeDelay);
            if (_panel.alpha == 1)
            {
                _fading = false;
            }
        }

        _panel.gameObject.SetActive(_fading || !fade);
    }

    public void SetFade(bool value)
    {
        fade = value;
    }
}
