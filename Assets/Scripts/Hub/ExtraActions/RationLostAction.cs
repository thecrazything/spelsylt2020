using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RationLostAction : IExtraAction
{
    public string GetMessage()
    {
        return TextConstants.RATION_LOST_MESSAGES[Random.Range(0, TextConstants.RATION_LOST_MESSAGES.Length)];
    }

    public SkillsEnum GetSkill()
    {
        return SkillsEnum.NEUTRAL;
    }

    public void on(Character character)
    {
        GameStatsService.Instance.gameStats.RemoveRation(1);
    }

    public void on(Player character)
    {
        throw new System.NotImplementedException();
    }
}
