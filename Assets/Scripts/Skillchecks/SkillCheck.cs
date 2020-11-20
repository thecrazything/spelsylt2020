using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillCheck
{
    private static readonly int baseDice = 3;

    public static Result DoCheck(Character character, SkillTest test)
    {
        int dice = baseDice;
        // TODO check disadvantage/advantage?

        if (character.skill == test.skill)
        {
            dice += 2;
        }

        int sucesses = 0;
        for (var i = 0; i < dice; i++)
        {
            int roll = doDiceRoll();
            if (roll == 6)
            {
                sucesses += 2;
            } 
            else if (roll == 4 || roll == 5)
            {
                sucesses += 1;
            }
        }

        // special extra dice that determines bonus/complication
        int special = doDiceRoll();
        bool bonus = false;
        bool complication = false;
        if (special == 6)
        {
            sucesses += 2;
            bonus = true;
        }
        else if (special == 4 || special == 5)
        {
            sucesses += 1;
        }
        if (special == 1)
        {
            complication = true;
        }

        return new Result(test.skill, sucesses >= test.difficulty, sucesses, bonus, complication);
    }

    private static int doDiceRoll()
    {
        return Random.Range(1, 7);
    }

    public class Result {

        public SkillsEnum skill { get; }
        public bool success { get; }
        public int sucessDice { get; }
        public bool bonus { get;  }
        public bool complication { get;  }

        public Result(SkillsEnum skill, bool sucess, int sucessDice, bool bonus, bool complication) {
            this.skill = skill;
            this.success = sucess;
            this.sucessDice = sucessDice;
            this.bonus = bonus;
            this.complication = complication;
        }
    }
}
