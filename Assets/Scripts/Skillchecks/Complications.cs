using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Complications
{
    private static readonly Complication[] complications = { new Complication("{name} missplaced a ration.", SkillsEnum.NEUTRAL, Consequence.RationChange(1)) };

    public static Complication getRandom(SkillsEnum skill)
    {
        Complication[] choice = complications.Where(x => x.skill == skill || x.skill == SkillsEnum.NEUTRAL).ToArray();
        return choice[Random.Range(0, choice.Length)];
    }
}
