﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Expedition;

public class Gem : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Picked up a gem!");
        Destroy(gameObject);
    }
}