using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Expedition;

public class Gem : MonoBehaviour, IInteractable
{
    public void Interact(GameObject source)
    {
        Debug.Log("Picked up a gem!");
        Destroy(gameObject);
    }

    public float? GetActionTime()
    {
        return null;
    }
}
