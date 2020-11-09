using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Expedition;

public class Interactor : MonoBehaviour
{
    IInteractable _focusedInteractable;

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
            _focusedInteractable.Interact(gameObject.transform.parent.gameObject);
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
