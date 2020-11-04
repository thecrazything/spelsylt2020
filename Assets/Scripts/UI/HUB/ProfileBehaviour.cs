using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProfileBehaviour : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public int characterId = 0;
    public Text nameField;

    private Character _character;
    // Start is called before the first frame update
    void Start()
    {
        _character = GameStatsService.Instance.GetCharacterById(characterId);
        if (_character == null)
        {
            throw new ArgumentException("No such character with id " + characterId);
        }
        nameField.text = _character.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameStatsService.Instance.selectedCharacter = _character;
    }

    public void OnDeselect(BaseEventData eventData)
    {
       if (GameStatsService.Instance.selectedCharacter != null && GameStatsService.Instance.selectedCharacter.id == _character.id)
        {
            GameStatsService.Instance.selectedCharacter = null;
        }
    }
}
