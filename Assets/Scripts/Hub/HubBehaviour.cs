using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class HubBehaviour : MonoBehaviour
{
    private Character _selectedCharacter;

    public Character selectedCharacter {
        get { return _selectedCharacter; }
        set 
        {
            onSelectedCharacterChange(value);
        } 
    }

    public BlackoutTextBehaviour blackoutTextBehaviour;
    public ConsoleBehaviour consoleBehaviour;
    public HubTask[] avalibleTasks = { };
    // Start is called before the first frame update
    void Start()
    {
        string startText = GameStatsService.Instance.gameStats.daysLeft == 7 ? TextConstants.INTRO_MESSAGE + "\n \n" : "";
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
        blackoutTextBehaviour.WriteText(startText);
        consoleBehaviour.WriteText(startText);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject() && selectedCharacter != null)
        {
            selectedCharacter = null;
        }
    }

    public void RedrawCharacter()
    {
        printCharacter(_selectedCharacter);
    }

    private void onSelectedCharacterChange(Character character)
    {
        if (_selectedCharacter == character)
        {
            return;
        }
        printCharacter(character);
        _selectedCharacter = character;
    }

    private void printCharacter(Character character)
    {
        if (character == null)
        {
            string txt = "";
            for (var i = 0; i < avalibleTasks.Length; i++)
            {
                txt += avalibleTasks[i].skillTest.description;
                if (avalibleTasks[i].doer != null)
                {
                    txt += " " + TextConstants.ASSIGNED_TO.Replace("{name}", avalibleTasks[i].doer.name);
                }
                txt += "\n";
            }
            if (GameStatsService.Instance.selectedCharacter != null)
            {
                txt += TextConstants.EXPEDITION_ASSIGNED_TO.Replace("{name}", GameStatsService.Instance.selectedCharacter.name) + "\n";
            }
            else
            {
                txt += TextConstants.EXPEDITION_UNASSIGNED + "\n";
            }
            txt += "\n" + TextConstants.IDLE_TEXT;
            consoleBehaviour.WriteTextWithSound(txt);
        }
        else
        {
            string txt = TextConstants.USER_DETAIL_NAME_TEXT + "\n" +
                CharacterTextFormatter.FormatSkill(character) + "\n" +
                CharacterTextFormatter.FormatHealth(character) + "\n" +
                CharacterTextFormatter.FormatMentalHealth(character) + "\n" +
                CharacterTextFormatter.FormatHunger(character);
            consoleBehaviour.WriteTextWithSound(txt.Replace("{name}", character.name));
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
                    task.skillTest.OnSuccess(task.doer);
                    tasksSummary += "\n" + task.skillTest.passMessage.Replace("{name}", task.doer.name);
                }
                else
                {
                    task.skillTest.OnFail(task.doer);
                    tasksSummary += "\n" + task.skillTest.failMessage.Replace("{name}", task.doer.name);
                }

                if (result.complication)
                {
                    IExtraAction complication = HubComplications.getRandom(task.skillTest.skill);
                    tasksSummary += "\n" + complication.GetMessage().Replace("{name}", task.doer.name);
                    complication.on(GameStatsService.Instance);
                }
                if (result.bonus)
                {
                    IExtraAction bonus = HubBoons.getRandom(task.skillTest.skill);
                    tasksSummary += "\n" + bonus.GetMessage().Replace("{name}", task.doer.name);
                    bonus.on(GameStatsService.Instance);
                }
            }
            else
            {
                task.skillTest.OnFail();
                tasksSummary += "\n" + task.skillTest.ignoreMessage;
            }
        }

        return TextConstants.NEXT_DAY_MESSAGE + "\n" + tasksSummary + "\n";
    }

    private string setupNewDay()
    {
        List<HubTask> tasks = new List<HubTask>();
        tasks.AddRange(HubTaskManager.getRandom(3, avalibleTasks.Select(x => x.skillTest).ToArray()));
        avalibleTasks = tasks.ToArray();

        string msg = "$ tasks -a \n TASKS requiring crew attention: \n ";
        for (var i = 0; i < avalibleTasks.Length; i++)
        {
            msg += avalibleTasks[i].skillTest.description + "\n ";
        }

        bool allDead = true;
        GameStatsService.Instance.characters.ToList().ForEach(character =>
        {
            character.hunger -= 1;
            if (!character.dead)
            {
                if (character.hunger <= 0)
                {
                    // Starved
                    character.dead = true;
                    msg += "\n" + TextConstants.STARVED.Replace("{name}", character.name);
                }
                if (character.health <= 0)
                {
                    // Just dead
                    character.dead = true;
                    msg += "\n" + TextConstants.DIED.Replace("{name}", character.name);
                }
                if (character.mentalHealth <= 0)
                {
                    // insane
                    character.dead = true;
                    msg += "\n" + TextConstants.INSANE.Replace("{name}", character.name);
                }
            }
            if (!character.dead)
            {
                allDead = false;
            }
        });

        if (allDead)
        {
            GameOver();
        }


        return msg;
    }

    private void GameOver()
    {
        // TODO you lost lol
    }
}
