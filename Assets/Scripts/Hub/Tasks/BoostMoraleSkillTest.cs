using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostMoraleSkillTest : HubSkillTest
{
    public BoostMoraleSkillTest() : base(SkillsEnum.LEADERSHIP, 
        SkillTest.DIFFICULTY_MEDIUM, 
        "Boost Morale", 
        "Boost the crews morale", 
        "{name} has lifed everyones spirits with a speech.", 
        "{name} tried to hold a speech but it demoralized the crew.", 
        "Noone tried to improve morale.")
    {
    }


    public override void OnFail(Character character)
    {
        foreach(Character c in GameStatsService.Instance.characters)
        {
            c.mentalHealth -= 1;
        }
    }

    public override void OnFail()
    {
    }

    public override void OnSuccess(Character character)
    {
        foreach (Character c in GameStatsService.Instance.characters)
        {
            c.mentalHealth += 2;
        }
    }
}
