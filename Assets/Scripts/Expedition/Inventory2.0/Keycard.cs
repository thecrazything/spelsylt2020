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

    public override string GetName()
    {
        return color + " keycard";
    }
}
