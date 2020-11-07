using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consequence
{
    public int rationChange { get; }
    private Consequence(int rationChange) 
    {
        this.rationChange = rationChange;
    }

    public static Consequence RationChange(int amount)
    {
        return new Consequence(amount);
    }
}
