using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RationLostAction : IExtraAction
{
    public string GetMessage()
    {
        return "{name} managed to lose a ration.";
    }

    public SkillsEnum GetSkill()
    {
        return SkillsEnum.NEUTRAL;
    }

    public void on(GameStatsService stats)
    {
        stats.gameStats.RemoveRation(1);
    }

    public void on(Player character)
    {
        throw new System.NotImplementedException();
    }
}
