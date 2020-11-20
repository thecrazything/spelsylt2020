using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCheckupSkillTest : HubSkillTest
{
    public HealthCheckupSkillTest() : base(SkillsEnum.MEDIC,
        SkillTest.DIFFICULTY_MEDIUM,
        "Crew health checkuo",
        "Give the crew a health checkup",
        "{name} gave the crew a health checkup.",
        "{name} failed giving the crew a health checkup.",
        "Noone gave the crew a health checkup.")
    {
    }

    public override void OnFail(Character character)
    {

    }

    public override void OnFail()
    {

    }

    public override void OnSuccess(Character character)
    {
        foreach (Character c in GameStatsService.Instance.characters)
        {
            c.addHealth(10);
        }
    }
}

