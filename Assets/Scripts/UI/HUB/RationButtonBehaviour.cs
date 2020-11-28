using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RationButtonBehaviour : MonoBehaviour
{
    public ProfileBehaviour pfb;
    private Button _button;
    private AudioSource _eatSound;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _eatSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _button.interactable = GameStatsService.Instance.gameStats.RationCount() > 0;
    }

    public void giveRation()
    {
        _eatSound.Play();
        GameStatsService.Instance.gameStats.RemoveRation();
        pfb.character.hunger += 1;
        pfb.hub.RedrawCharacter();
    }
}
