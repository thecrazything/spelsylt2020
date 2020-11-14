using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExpeditionComplications
{
    private static readonly IExtraAction[] complications = { new OxygenLostAction() };

    public static IExtraAction getRandom(SkillsEnum skill)
    {
        IExtraAction[] choice = complications.Where(x => x.GetSkill() == skill || x.GetSkill() == SkillsEnum.NEUTRAL).ToArray();
        return choice[Random.Range(0, choice.Length)];
    }
}
