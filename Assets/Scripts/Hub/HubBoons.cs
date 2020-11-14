using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HubBoons
{
    private static readonly IExtraAction[] boons = { new RationFoundAction() };

    public static IExtraAction getRandom(SkillsEnum skill)
    {
        IExtraAction[] choice = boons.Where(x => x.GetSkill() == skill || x.GetSkill() == SkillsEnum.NEUTRAL).ToArray();
        return choice[Random.Range(0, choice.Length)];
    }
}
