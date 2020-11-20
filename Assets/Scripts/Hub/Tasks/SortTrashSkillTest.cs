using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortTrashSkillTest : HubSkillTest
{
    public SortTrashSkillTest() : base(SkillsEnum.UTILITY,
        SkillTest.DIFFICULTY_VERY_HARD,
        "Sort through stash",
        "Sort through the hub stash",
        "{name} sort through stash and found a ration!",
        "{name} sorted through the HUB stash but found nothing.",
        "Noone sorted through the HUB stash.")
    {
    }

    public override void OnFail()
    {

    }

    public override void OnSuccess(Character character)
    {
       GameStatsService.Instance.gameStats.AddItem(new Ration());
    }
}
