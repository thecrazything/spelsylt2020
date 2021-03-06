﻿using System;
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
    private Func<bool, bool> _onComplete;

    public TextPrintAnimation(Text textView, float delay)
    {
        _textView = textView;
        _delay = delay;

        if (_textView == null)
        {
            throw new ArgumentNullException("TextView");
        }
    }

    public void Write(string text, Func<bool, bool> onComplete)
    {
        _onComplete = onComplete;
        Write(text);
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
                _onComplete?.Invoke(true);
                _textToWrite = null;
                _onComplete = null;
            }
            else if (_currentWriteTime >= _delay)
            {
                _currentWriteTime = 0;
                _textView.text += _textToWrite[_currentPosition++];
            }
        }
    }

    public static string spaceLetters(string text)
    {
        string result = "";
        for (var i = 0; i <= text.Length - 1; i++)
        {
            result += text[i] + ( i >= (text.Length - 1) ? "" : " ");
        }
        return result;
    }

    public void Clear()
    {
        _textView.text = "";
    }
}
