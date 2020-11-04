using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleBehaviour : MonoBehaviour
{
    public float writeDelay = 0.1f;
    private Text _consoleText;

    private string defaultText = "Much need to be done in the HUB and on expeditions or whatever lol.";
    private string characterSelectedText = "{name} is feeling pretty swell. Pretty swiggity swooty. Hip with the kids.";

    private int _currentPosition;
    private string _textToWrite;

    private float _currentWriteTime;

    // Start is called before the first frame update
    void Start()
    {
        _consoleText = GetComponent<Text>();
        _consoleText.text = defaultText;
        GameStatsService.Instance.onChangeSelectedCharacter += character =>
        {
            if (character == null)
            {
                WriteText(defaultText);
            } 
            else
            {
                WriteText(characterSelectedText.Replace("{name}", character.name));
            }
        };
    }

    void WriteText(string text)
    {
        _consoleText.text = "";
        _currentPosition = 0;
        _textToWrite = text;
        _currentWriteTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_textToWrite != null)
        {
            _currentWriteTime += Time.deltaTime;
            if (_currentPosition >= _textToWrite.Length)
            {
                _textToWrite = null;
            } 
            else if (_currentWriteTime >= writeDelay)
            {
                _currentWriteTime = 0;
                _consoleText.text += _textToWrite[_currentPosition++];
            }
        }
    }
}
