using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ProfileBehaviour : MonoBehaviour, ISelectHandler
{
    public HubBehaviour hub;
    public Image dropdown;
    public Image taskDropdown;
    public int characterId = 0;
    public Text nameField;
    public Image profilePictureBackground;
    public Color disableColor;

    public Character character;
    private Selectable _selectable;
    private Color _ProfilePictureBackgroundColor;
    // Start is called before the first frame update
    void Start()
    {
        hub = GameObject.Find("Main Camera").GetComponent<HubBehaviour>();
        _selectable = GetComponent<Selectable>();
        character = GameStatsService.Instance.GetCharacterById(characterId);
        if (character == null)
        {
            throw new ArgumentException("No such character with id " + characterId);
        }
        nameField.text = TextPrintAnimation.spaceLetters(character.name);
        _ProfilePictureBackgroundColor = profilePictureBackground.color;
        if (character.dead)
        {
            _selectable.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hub.selectedCharacter != null && hub.selectedCharacter.id == character.id)
        {
            dropdown.gameObject.SetActive(true);
        }
        if (hub.selectedCharacter != null && hub.selectedCharacter.id != character.id)
        {
            _selectable.interactable = false;
            profilePictureBackground.color = disableColor;
            dropdown.gameObject.SetActive(false);
            taskDropdown.gameObject.SetActive(false);
        } 
        else if (!character.dead)
        {
            _selectable.interactable = true;
            profilePictureBackground.color = _ProfilePictureBackgroundColor;
        }
        if(hub.selectedCharacter == null)
        {
            dropdown.gameObject.SetActive(false);
            taskDropdown.gameObject.SetActive(false);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        hub.selectedCharacter = character;
    }
}
