using System;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public Slider interactProgressbar;

    IInteractable _focusedInteractable;
    Player player;

    bool _isInteracting = false;
    float _timeout;
    float _time = 0;
    PlayerMovement _playerMovement;

    void Start()
    {
        // TODO: Inte säker på att denna behövs, från MC
        player = GetComponentInParent<Player>();

        if (interactProgressbar == null)
        {
            throw new ArgumentNullException("No Progressbar found");
        }
        interactProgressbar.gameObject.SetActive(false);
        _playerMovement = transform.parent.gameObject.GetComponent<PlayerMovement>();

        if (_playerMovement == null)
        {
            throw new ArgumentNullException("No Player component found");
        }
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
            if (_focusedInteractable.GetActionTime() != null)
            {
                _playerMovement.SetFrozen(true);
                _isInteracting = true;
                _timeout = (float)_focusedInteractable.GetActionTime();
                _time = 0;
                interactProgressbar.maxValue = _timeout;
                interactProgressbar.gameObject.SetActive(true);
            }
            else
            {
                _focusedInteractable.Interact(gameObject.transform.parent.gameObject);
            }
        }
        else {
            Debug.Log("Nothing to interact with");
        }
    }

    void StopInteract()
    {
        _playerMovement.SetFrozen(false);
        _isInteracting = false;
        interactProgressbar.gameObject.SetActive(false);
    }

    void ProgressInteraction()
    {
        _time += Time.deltaTime;
        interactProgressbar.value = _time;
        if (_time >= _timeout)
        {
            StopInteract();
            _focusedInteractable.Interact(gameObject.transform.parent.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out _focusedInteractable)) {
            Debug.Log("Found an interactable");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        _focusedInteractable = null;
    }
}
