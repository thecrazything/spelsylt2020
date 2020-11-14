using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HubComplications
{
    private static readonly IExtraAction[] complications = { new RationLostAction() };

    public static IExtraAction getRandom(SkillsEnum skill)
    {
        IExtraAction[] choice = complications.Where(x => x.GetSkill() == skill || x.GetSkill() == SkillsEnum.NEUTRAL).ToArray();
        return choice[Random.Range(0, choice.Length)];
    }
}
