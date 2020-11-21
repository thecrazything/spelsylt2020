using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeycardColor {
    Green,
    Blue
}

public class Keycard : Item
{
    public KeycardColor color;

    public Keycard(KeycardColor color)
    {
        this.color = color;
    }

    public override string GetName()
    {
        return color + " keycard";
    }
}
