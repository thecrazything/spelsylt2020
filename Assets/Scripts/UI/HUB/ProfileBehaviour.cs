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
    public Image profilePictureBackground;
    public Color disableColor;

    private Character _character;
    private Selectable _selectable;
    private Color _ProfilePictureBackgroundColor;
    // Start is called before the first frame update
    void Start()
    {
        _selectable = GetComponent<Selectable>();
        _character = GameStatsService.Instance.GetCharacterById(characterId);
        if (_character == null)
        {
            throw new ArgumentException("No such character with id " + characterId);
        }
        nameField.text = TextPrintAnimation.spaceLetters(_character.name);
        _ProfilePictureBackgroundColor = profilePictureBackground.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameStatsService.Instance.tmpSelectedCharacter != null && GameStatsService.Instance.tmpSelectedCharacter.id != _character.id)
        {
            _selectable.interactable = false;
            profilePictureBackground.color = disableColor;
        } else
        {
            _selectable.interactable = true;
            profilePictureBackground.color = _ProfilePictureBackgroundColor;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        GameStatsService.Instance.tmpSelectedCharacter = _character;
        GameStatsService.Instance.selectedCharacter = _character; // BAD AND UGLY
    }

    public void OnDeselect(BaseEventData eventData)
    {
       if (GameStatsService.Instance.tmpSelectedCharacter != null && GameStatsService.Instance.tmpSelectedCharacter.id == _character.id)
       {
            GameStatsService.Instance.tmpSelectedCharacter = null;
       }
    }
}
