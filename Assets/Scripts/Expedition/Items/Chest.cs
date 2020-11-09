using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Expedition;

public class Chest : MonoBehaviour, IInteractable
{
    public void Interact(GameObject source)
    {
        Debug.Log("Hello! I'm a chest");
    }
}
