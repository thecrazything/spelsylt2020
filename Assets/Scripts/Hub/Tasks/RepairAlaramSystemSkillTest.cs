using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairAlarmSystemSkillTest : HubSkillTest
{
    public RepairAlarmSystemSkillTest() : base(SkillsEnum.UTILITY,
        SkillTest.DIFFICULTY_MEDIUM,
        "Fix alarm system",
        "Fix the alarm system that keeps going off.",
        "{name} fixed the alaram system. It is now quiet.",
        "{name} failed to fix the alaram system at it is still going off.",
        "The alarm system is still beeping annoying everyone.")
    {
    }

    public override void OnFail()
    {
        foreach (Character c in GameStatsService.Instance.characters)
        {
            c.mentalHealth -= 1;
        }
    }

    public override void OnSuccess(Character character)
    {
    }
}
