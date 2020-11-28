﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RationFoundAction : IExtraAction
{
    public string GetMessage()
    {
        return "{name} found a ration!";
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
