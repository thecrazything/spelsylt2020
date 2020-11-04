using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPrintAnimation
{
    private Text _textView;
    private float _delay;
    private string _textToWrite;
    private int _currentPosition = 0;
    private float _currentWriteTime = 0.0f;

    public TextPrintAnimation(Text textView, float delay)
    {
        _textView = textView;
        _delay = delay;
    }

    public void Write(string text)
    {
        _textView.text = "";
        _textToWrite = text;
        _currentPosition = 0;
        _currentWriteTime = 0.0f;

    }

    public void printNextIfTime(float deltaTime)
    {
        if (_textToWrite != null)
        {
            _currentWriteTime += Time.deltaTime;
            if (_currentPosition >= _textToWrite.Length)
            {
                _textToWrite = null;
            }
            else if (_currentWriteTime >= _delay)
            {
                _currentWriteTime = 0;
                _textView.text += _textToWrite[_currentPosition++];
            }
        }
    }
}
