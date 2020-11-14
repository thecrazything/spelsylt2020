using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ExpeditionBoons
{
    private static readonly IExtraAction[] boons = { new BreathHeldAction() };

    public static IExtraAction getRandom(SkillsEnum skill)
    {
        IExtraAction[] choice = boons.Where(x => x.GetSkill() == skill || x.GetSkill() == SkillsEnum.NEUTRAL).ToArray();
        return choice[Random.Range(0, choice.Length)];
    }
}
