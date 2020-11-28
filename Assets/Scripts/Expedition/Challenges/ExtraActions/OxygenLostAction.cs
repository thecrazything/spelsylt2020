using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenLostAction : IExtraAction
{
    public string GetMessage()
    {
        return "{name} wasted some oxygen performing the action.";
    }

    public SkillsEnum GetSkill()
    {
        return SkillsEnum.NEUTRAL;
    }

    public void on(Character stats)
    {
        throw new System.NotImplementedException();
    }

    public void on(Player player)
    {
        player.oxygen += Random.Range(1, 10);
    }
}
