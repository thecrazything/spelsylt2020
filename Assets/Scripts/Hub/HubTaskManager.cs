using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HubTaskManager
{
    private static SkillTest[] skillTests = { 
        new SkillTest(SkillsEnum.TECHINCAL, SkillTest.DIFFICULTY_HARD, "Repair Ventilation", "HUB Ventilation is broken.", "{name} repaired the HUB ventilation. Air quality at 90%.", "{name} failed to repair the ventilation. Air quality is at 20%."),
        new SkillTest(SkillsEnum.NEUTRAL, SkillTest.DIFFICULTY_MEDIUM, "Boost moral", "The crew needs increased moral.", "{name} boosted the crews moral with a speech.", "{name} failed to boost moral."),
        new SkillTest(SkillsEnum.NEUTRAL, SkillTest.DIFFICULTY_EASY, "Clean HUB", "HUB needs cleaning.", "{name} tidied up the HUB.", "{name} could not clean the HUB.")
    };
    public static HubTask[] getRandom(int amount, SkillTest[] exclude)
    {
        HubTask[] tasks = new HubTask[amount];
        for (var i = 0; i < tasks.Length; i++)
        {
            SkillTest test = getRandomTest(exclude, 0);
            tasks[i] = new HubTask();
            tasks[i].skillTest = test;
        }
        return tasks;
    }

    private static SkillTest getRandomTest(SkillTest[] exclude, int tries)
    {
        SkillTest test = skillTests[Random.Range(0, skillTests.Length)];
        if (exclude.Contains(test) && tries <= 10)
        {
            return getRandomTest(exclude, tries + 1);
        }
        return test;
    }
}
