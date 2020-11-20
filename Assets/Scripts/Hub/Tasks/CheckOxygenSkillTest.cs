using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOxygenSkillTest : HubSkillTest
{
    public CheckOxygenSkillTest() : base(SkillsEnum.MECHANIC,
        SkillTest.DIFFICULTY_MEDIUM,
        "Check oxygen levels",
        "Check the HUB oxygen levels.",
        "{name} checked the oxygen leves and they are fine.",
        "{name} checked the oxygen levels. The failing gauge created some panic.",
        "Noone checked the oxygen levels.")
    {
    }

    public override void OnFail(Character character)
    {
        character.mentalHealth -= 1;
        OnFail();
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
        foreach (Character c in GameStatsService.Instance.characters)
        {
            c.mentalHealth += 1;
        }
    }
}
