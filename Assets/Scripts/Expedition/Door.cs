using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    bool _isOpen;
    Animator animator;
    
    public GameObject shadowObject;

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
        _isOpen = true;
        shadowObject.SetActive(false);
        animator.SetBool("IsOpen", true);
    }
}
