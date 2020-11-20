using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanRationsSkillTest : HubSkillTest
{
    public PlanRationsSkillTest() : base(SkillsEnum.LEADERSHIP,
        SkillTest.DIFFICULTY_MEDIUM,
        "Plan rations",
        "Plan rations portion size.",
        "{name} planned the rations portion sizes.",
        "{name} tried to plan the rations portion sizes but it did not add up.",
        "Noone planned the ration sizes.")
    {
    }

    public override void OnFail(Character character)
    {
        GameStatsService.Instance.gameStats.RemoveRation();
    }

    public override void OnFail()
    {

    }

    public override void OnSuccess(Character character)
    {
        GameStatsService.Instance.gameStats.AddItem(new Ration());
    }
}
