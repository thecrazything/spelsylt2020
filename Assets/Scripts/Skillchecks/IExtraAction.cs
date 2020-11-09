using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExtraAction
{
    void on(GameStatsService stats);
    void on(Player character);

    SkillsEnum GetSkill();
    string GetMessage();
}
