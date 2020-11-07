using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consequence
{
    public int rationLoss { get; }
    private Consequence(int rationLoss) { }

    public static Consequence RationLosss(int amount)
    {
        return new Consequence(amount);
    }
}
