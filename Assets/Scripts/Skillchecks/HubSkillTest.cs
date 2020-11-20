using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HubSkillTest : SkillTest
{
    public readonly string ignoreMessage;
    public HubSkillTest(SkillsEnum skill, int difficulty, string name, string description, string passMessage, string failMessage, string ignoreMessage) : base(skill, difficulty, name, description, passMessage, failMessage)
    {
        this.ignoreMessage = ignoreMessage;
    }

    public abstract void OnSuccess(Character character);
    public virtual void OnFail(Character character)
    {
        OnFail();
    }
    public abstract void OnFail();
}
