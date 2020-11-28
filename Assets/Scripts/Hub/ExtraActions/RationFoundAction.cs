using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RationFoundAction : IExtraAction
{
    public string GetMessage()
    {
        return TextConstants.RATION_FOUND_MESSAGES[Random.Range(0, TextConstants.RATION_FOUND_MESSAGES.Length)];
    }

    public SkillsEnum GetSkill()
    {
        return SkillsEnum.NEUTRAL;
    }

    public void on(Character stats)
    {
        GameStatsService.Instance.gameStats.AddItem(new Ration());
    }

    public void on(Player character)
    {
        throw new System.NotImplementedException();
    }
}
