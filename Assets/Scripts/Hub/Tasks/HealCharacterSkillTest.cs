using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCharacterSkillTest : HubSkillTest
{
    Character target;
    public HealCharacterSkillTest(Character character) : base(SkillsEnum.LEADERSHIP,
        SkillTest.DIFFICULTY_HARD,
        "Heal " + character.name,
        "Heal " + character.name + ".",
        "{name} fixed " + character.name + "s wounds.",
        "{name} tried to fix " + character.name + "s wounds but had no success.",
        character.name + "s wounds remain ignored.")
    {
        target = character;
    }

    public override void OnFail(Character character)
    {
    }

    public override void OnFail()
    {
    }

    public override void OnSuccess(Character character)
    {
        target.addHealth(20);
    }

    public Character GetCharacter()
    {
        return target;
    }
}
