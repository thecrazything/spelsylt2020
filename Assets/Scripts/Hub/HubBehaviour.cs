using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class HubBehaviour : MonoBehaviour
{
    public Character selectedCharacter;
    public ConsoleBehaviour consoleBehaviour;
    public HubTask[] avalibleTasks = { };
    // Start is called before the first frame update
    void Start()
    {
        string startText = GameStatsService.Instance.gameStats.daysLeft == 12 ? TextConstants.INTRO_MESSAGE + "\n \n" : "";
        if (GameStatsService.Instance.gameStats == null)
        {
            throw new ArgumentNullException("No Gamestats found in GameStatsService");
        }
        if (GameStatsService.Instance.gameStats.expeditionComplete)
        {
            startText = tallyLastDay();
        }
        GameStatsService.Instance.gameStats.expeditionComplete = false;
        startText += setupNewDay();
        consoleBehaviour.WriteText(startText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            selectedCharacter = null;
        }
    }

    private string tallyLastDay()
    {
        GameStatsService.Instance.gameStats.newDay();
        GameStatsService.Instance.selectedCharacter = null;
        HubTask[] previousTasks = GameStatsService.Instance.gameStats.hubTasks;
        string tasksSummary = "";
        for (var i = 0; i < previousTasks.Length; i++)
        {
            HubTask task = previousTasks[i];
            if (task.doer != null)
            {
                SkillCheck.Result result = SkillCheck.DoCheck(task.doer, task.skillTest);
                if (result.success)
                {
                    // TODO test success consequence
                    tasksSummary += "\n" + task.skillTest.passMessage.Replace("{name}", task.doer.name);
                }
                else
                {
                    // TODO test failure consequence
                    tasksSummary += "\n" + task.skillTest.failMessage.Replace("{name}", task.doer.name);
                }

                if (result.complication != null)
                {
                    tasksSummary += "\n" + result.complication.message.Replace("{name}", task.doer.name);
                    // TODO performe complicaiton consequence
                }
            }
            else
            {
                // TODO failure consequence
                tasksSummary += "\n" + task.skillTest.failMessage.Replace("{name}", ""); // TODO Make the print nicer than just removing the name
            }
        }

        return TextConstants.NEXT_DAY_MESSAGE + "\n" + tasksSummary + "\n";
    }

    private string setupNewDay()
    {
        HubTask[] tasks = HubTaskManager.getRandom(3, avalibleTasks.Select(x => x.skillTest).ToArray());
        avalibleTasks = tasks;
        string msg = "$ tasks -a \n TASKS requiring crew attention: \n ";
        for (var i = 0; i < avalibleTasks.Length; i++)
        {
            msg += avalibleTasks[i].skillTest.description + "\n ";
        }

        GameStatsService.Instance.characters.ToList().ForEach(character =>
        {
            character.hunger -= 1;
        });

        // TODO announce deaths (and the cause)

        return msg;
    }
}
