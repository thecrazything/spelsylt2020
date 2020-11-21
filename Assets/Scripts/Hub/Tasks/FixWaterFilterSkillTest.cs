using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixWaterSkillTest : HubSkillTest
{
    public FixWaterSkillTest() : base(SkillsEnum.MECHANIC,
        SkillTest.DIFFICULTY_MEDIUM,
        "Fix water filter",
        "Fix the water filter",
        "{name} fixed the failing water filter.",
        "{name} failed to fix the water filter.",
        "Noone fixed the failing water filter.")
    {
    }

    public override void OnFail()
    {
        foreach (Character c in GameStatsService.Instance.characters)
        {
            c.subtractHealth(20);
        }
    }

    public override void OnSuccess(Character character)
    {
    }
}
