using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boons
{
    private static readonly Boon[] boons = { new Boon("{name} found a ration!", SkillsEnum.NEUTRAL, Consequence.RationChange(1)) };

    public static Boon getRandom(SkillsEnum skill)
    {
        Boon[] choice = boons.Where(x => x.skill == skill || x.skill == SkillsEnum.NEUTRAL).ToArray();
        return choice[Random.Range(0, choice.Length)];
    }
}
