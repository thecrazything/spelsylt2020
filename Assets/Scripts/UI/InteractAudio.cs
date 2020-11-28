using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractAudio : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public bool confimSound = false;
    private AudioClip _onHover;
    private AudioClip _onClick;
    private AudioClip _onConfirm;
    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = gameObject.AddComponent<AudioSource>();
        _source.volume = 0.1f;
        _onHover = (AudioClip)Resources.Load("Sounds/Menu/hover");
        _onClick = (AudioClip)Resources.Load("Sounds/Menu/click");
        _onConfirm = (AudioClip)Resources.Load("Sounds/Menu/confirm");
    }

    public void OnHover()
    {
        _source.clip = _onHover;
        _source.Play();
    }

    public void OnClick()
    {
        _source.clip = _onClick;
        _source.Play();
    }

    public void OnConfirm()
    {
        _source.clip = _onConfirm;
        _source.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (confimSound)
        {
            OnConfirm();
        }
        else
        {
            OnClick();
        }
    }
}
