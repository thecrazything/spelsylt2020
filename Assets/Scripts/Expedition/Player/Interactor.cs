using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    IInteractable _focusedInteractable;
    Player player;

    void Start() {
        player = GetComponentInParent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            TryInteract();
        }
    }

    void TryInteract()
    {
        if (_focusedInteractable != null)
        {
            _focusedInteractable.Interact(player);
        }
        else {
            Debug.Log("Nothing to interact with");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("asdf");
        if (collider.TryGetComponent(out _focusedInteractable)) {
            Debug.Log("Found an interactable");
        }
    }

    void OnTriggerExit(Collider collider)
    {
        _focusedInteractable = null;
    }
}
