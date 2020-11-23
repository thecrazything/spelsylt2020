using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactor : MonoBehaviour
{
    public Slider interactProgressbar;
    public GameObject interactTextObject;
    public GameObject canInteractPromt;

    TextMeshProUGUI _interactText;
    IInteractable _focusedInteractable;
    Player player;

    public event EventHandler<OnPlayerLostInteractFocus> OnLostInteractFocus;
    public class OnPlayerLostInteractFocus : EventArgs
    {}

    bool _isInteracting = false;
    float _timeout;
    float _time = 0;
    PlayerMovement _playerMovement;
    AudioSource _audioSource;

    void Start()
    {
        // TODO: Inte säker på att denna behövs, från MC
        player = GetComponentInParent<Player>();
        _interactText = interactTextObject.GetComponent<TextMeshProUGUI>();

        if (interactProgressbar == null)
        {
            throw new ArgumentNullException("No Progressbar found");
        }
        interactProgressbar.gameObject.SetActive(false);
        _playerMovement = transform.parent.parent.gameObject.GetComponent<PlayerMovement>();

        if (_playerMovement == null)
        {
            throw new ArgumentNullException("No Player component found");
        }
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // TODO map to inputs
        // TODO interrupt on any move
        if (Input.GetKeyDown(KeyCode.E)) {
            if (!_isInteracting)
            {
                TryInteract();
            }
            else
            {
                StopInteract();
            }
        }

        if (_isInteracting)
        {
            ProgressInteraction();
        }
    }

    void TryInteract()
    {
        if (_focusedInteractable != null)
        {
            canInteractPromt.SetActive(false);

            AudioClip sound = _focusedInteractable.GetActionSound(gameObject.transform.parent.parent.gameObject);
            if (sound != null)
            {
                _audioSource.clip = sound;
                _audioSource.Play();
            }

            float? actionTime = _focusedInteractable.GetActionTime(gameObject.transform.parent.parent.gameObject);
            if (actionTime != null)
            {
                string title = _focusedInteractable.GetActionTitle(gameObject.transform.parent.parent.gameObject);
                if (title != null)
                {
                    SetAndActivateTitle(title);
                }

                _playerMovement.SetFrozen(true);
                _isInteracting = true;
                _timeout = (float)actionTime;
                _time = 0;
                interactProgressbar.maxValue = _timeout;
                interactProgressbar.gameObject.SetActive(true);
            }
            else
            {
                _focusedInteractable.Interact(gameObject.transform.parent.parent.gameObject);
            }
        }
        else
        {
            Debug.Log("Nothing to interact with");
        }
    }

    void SetAndActivateTitle(string title)
    {
        _interactText.text = title;
        interactTextObject.SetActive(true);
    }

    void RemoveAndDeactivateTitle()
    {
        _interactText.text = "";
        interactTextObject.SetActive(false);
    }

    void StopInteract()
    {
        _playerMovement.SetFrozen(false);
        _isInteracting = false;
        interactProgressbar.gameObject.SetActive(false);
        RemoveAndDeactivateTitle();
    }

    void ProgressInteraction()
    {
        _time += Time.deltaTime;
        interactProgressbar.value = _time;
        if (_time >= _timeout)
        {
            StopInteract();
            _focusedInteractable.Interact(gameObject.transform.parent.parent.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        IInteractable interactable;
        if (collider.TryGetComponent(out interactable)) {
            canInteractPromt.SetActive(true);
            _focusedInteractable = interactable;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        OnLostInteractFocus?.Invoke(this, new OnPlayerLostInteractFocus());

        IInteractable _exitInteractable = collider.GetComponent<IInteractable>();
        if (_exitInteractable != null && _exitInteractable == _focusedInteractable)
        {
            _focusedInteractable = null;
        }
        canInteractPromt.SetActive(false);
    }
}
