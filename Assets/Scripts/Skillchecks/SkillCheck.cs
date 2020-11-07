using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillCheck
{
    private static readonly int baseDice = 3;

    public static Result DoCheck(Character character, SkillTest test)
    {
        int dice = baseDice;
        // TODO check character skill and add on extra dice
        // TODO check disadvantage/advantage?

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

        return new Result(sucesses >= test.difficulty, sucesses, bonus ? Boons.getRandom(test.skill) : null, complication ? Complications.getRandom(test.skill) : null);
    }

    private static int doDiceRoll()
    {
        return Random.Range(1, 7);
    }

    public class Result {

        public bool success { get; }
        public int sucessDice { get; }
        public Boon bonus { get;  }
        public Complication complication { get;  }

        public Result(bool sucess, int sucessDice, Boon bonus, Complication complication) {
            this.success = sucess;
            this.sucessDice = sucessDice;
            this.bonus = bonus;
            this.complication = complication;
        }
    }
}
