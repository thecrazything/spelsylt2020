using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixVentilationSkillTest : HubSkillTest
{
    public FixVentilationSkillTest() : base(SkillsEnum.MECHANIC,
        SkillTest.DIFFICULTY_HARD,
        "Fix ventilation",
        "Fix the ventilation",
        "{name} has fixed the ventilation system. Air quality normal.",
        "{name} failed to repair the ventilation. Air quality 20%.",
        "The ventilation was not repaired. Air quality 20%.")
    {
    }


    public override void OnFail(Character character)
    {
        OnFail();
    }

    public override void OnFail()
    {
        foreach (Character c in GameStatsService.Instance.characters)
        {
            c.mentalHealth -= 2;
        }
    }

    public override void OnSuccess(Character character)
    {
    }
}
