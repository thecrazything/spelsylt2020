using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestSkillTest : HubSkillTest
{
    public RestSkillTest() : base(SkillsEnum.NEUTRAL, 0, "Rest", "Take a rest", "{} has rested.", "REST FAIL NOT A THING.", "IGNORE NOT A ThING")
    {
    }

    public override void OnSuccess(Character character)
    {
        character.mentalHealth += 1;
    }

    public override void OnFail()
    {
        throw new System.NotImplementedException();
    }
}
