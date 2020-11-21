using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanHUBTask : HubSkillTest
{
    public CleanHUBTask() : base(SkillsEnum.UTILITY,
        SkillTest.DIFFICULTY_HARD,
        "Clean HUB",
        "Clean the HUB.",
        "{name} has cleaned the HUB.",
        "{name} failed to clean the HUB.",
        "The HUB was left messy.")
    {
    }


    public override void OnFail(Character character)
    {
        OnFail();
    }

    public override void OnFail()
    {

    }

    public override void OnSuccess(Character character)
    {
        foreach (Character c in GameStatsService.Instance.characters)
        {
            c.mentalHealth -= 1;
        }
    }
}
