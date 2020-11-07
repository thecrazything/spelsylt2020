using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Complication
{
    public string message { get; }

    // Neutral means it can happen for any skillcheck
    public SkillsEnum skill { get;  }

    public Consequence consequence { get;  }

    public Complication(string message, SkillsEnum skill, Consequence consequence)
    {
        this.message = message;
        this.skill = skill;
        this.consequence = consequence;
    }
}
