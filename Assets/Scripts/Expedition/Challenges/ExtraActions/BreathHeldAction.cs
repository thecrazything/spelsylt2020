using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathHeldAction : IExtraAction
{
    public string GetMessage()
    {
        return "{name} managed to hold their breath, wasting less oxygen!";
    }

    public SkillsEnum GetSkill()
    {
        return SkillsEnum.NEUTRAL;
    }

    public void on(GameStatsService stats)
    {
        throw new System.NotImplementedException();
    }

    public void on(Player player)
    {
        player.oxygen += Random.Range(1, 5);
    }
}
