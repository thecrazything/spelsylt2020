using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleBehaviour : MonoBehaviour
{

    private Text _consoleText;

    private string defaultText = "Much need to be done in the HUB and on expeditions or whatever lol.";
    private string characterSelectedText = "{name} is feeling pretty swell. Pretty swiggity swooty. Hip with the kids.";

    // Start is called before the first frame update
    void Start()
    {
        _consoleText = GetComponent<Text>();
        _consoleText.text = defaultText;
        GameStatsService.Instance.onChangeSelectedCharacter += character =>
        {
            if (character == null)
            {
                _consoleText.text = defaultText;
            } 
            else
            {
                _consoleText.text = characterSelectedText.Replace("{name}", character.name);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
