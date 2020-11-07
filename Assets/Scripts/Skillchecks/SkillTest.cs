using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTest
{
    public static readonly int DIFFICULTY_EASY = 2;
    public static readonly int DIFFICULTY_STANDARD = 3;
    public static readonly int DIFFICULTY_MEDIUM = 4;
    public static readonly int DIFFICULTY_HARD = 5;
    public static readonly int DIFFICULTY_VERY_HARD = 7;
    public static readonly int DIFFICULTY_ALMOST_IMPOSSIBLE = 9;
    public SkillsEnum skill { get; }
    public int difficulty { get; }
    public string name { get; }
    public string description { get; }
    public string passMessage { get; }
    public string failMessage { get; }


    public SkillTest(SkillsEnum skill, int difficulty, string name, string description, string passMessage, string failMessage)
    {
        this.skill = skill;
        this.difficulty = difficulty;
        this.name = name;
        this.description = description;
        this.passMessage = passMessage;
        this.failMessage = failMessage;
    }
}
