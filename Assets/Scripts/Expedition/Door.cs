using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door : MonoBehaviour, IInteractable
{
    bool _isOpen;
    Animator animator;
    
    public GameObject shadowObject;
    public KeycardColor keycardColor;

    void Start() {
        animator = GetComponent<Animator>();
    }

    public float? GetActionTime()
    {
        return null;
    }

    public string GetActionTitle()
    {
        return null;
    }

    public void Interact(GameObject source)
    {
        Item keycard = source.GetComponent<Player>().inventory.items.Find(i => (i is Keycard) && (i as Keycard).color == keycardColor);

        if (keycard == null) return;

        _isOpen = true;
        shadowObject.SetActive(false);
        animator.SetBool("IsOpen", true);
    }
}
