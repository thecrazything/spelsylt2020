using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HubTaskManager
{
    private static HubSkillTest[] skillTests = {
        new BoostMoraleSkillTest(),
        new FixVentilationSkillTest(),
        new CleanHUBTask(),
        new SortTrashSkillTest(),
        new FixWaterSkillTest(),
        new RepairAlarmSystemSkillTest(),
        new CheckOxygenSkillTest(),
        new PlanRationsSkillTest(),
        new HealthCheckupSkillTest()
    };
    public static List<HubTask> getRandom(int amount, HubSkillTest[] exclude)
    {
        List<HubTask> tasks = new List<HubTask>();
        List<HubSkillTest> excluded = new List<HubSkillTest>(exclude);
        for (var i = 0; i < amount; i++)
        {
            HubSkillTest test = getRandomTest(excluded, 0);
            excluded.Add(test);
            HubTask task = new HubTask(); ;
            task.skillTest = test;
            tasks.Add(task);
        }
        return tasks;
    }

    private static HubSkillTest getRandomTest(List<HubSkillTest> exclude, int tries)
    {
        HubSkillTest test = skillTests[Random.Range(0, skillTests.Length)];
        if (exclude.Contains(test) && tries <= 100)
        {
            return getRandomTest(exclude, tries + 1);
        }
        return test;
    }
}
