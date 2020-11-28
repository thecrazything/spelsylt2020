using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExtraAction
{
    void on(Character character);
    void on(Player character);

    SkillsEnum GetSkill();
    string GetMessage();
}
