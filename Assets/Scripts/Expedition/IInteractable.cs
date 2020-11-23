using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

interface IInteractable
{
    void Interact(GameObject source);

    float? GetActionTime(GameObject source);

    string GetActionTitle(GameObject source);

    AudioClip GetActionSound(GameObject source);
}
