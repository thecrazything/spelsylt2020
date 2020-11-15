using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public void Interact(GameObject source)
    {
        Debug.Log("Hello! I'm a chest");
    }

    public float? GetActionTime()
    {
        return null;
    }

    public string GetActionTitle()
    {
        return null;
    }
}
