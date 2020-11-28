using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class HubBehaviour : MonoBehaviour
{
    private Character _selectedCharacter;

    public List<Character> restingCharacters = new List<Character>();
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
        if (GameStatsService.Instance.gameStats.victoryCondition)
        {
            Victory();
            return;
        }
        else if (GameStatsService.Instance.gameStats.daysLeft == 0)
        {
            GameOver(GameOverReason.TIMEOUT);
            return;
        }
        else
        {
            string startText = GameStatsService.Instance.gameStats.daysLeft == 7 ? TextConstants.INTRO_MESSAGE + "\n \n" : "";
            if (GameStatsService.Instance.gameStats == null)
            {
                throw new ArgumentNullException("No Gamestats found in GameStatsService");
            }
            if (GameStatsService.Instance.gameStats.expeditionComplete)
            {
                startText = tallyLastDay();
                GameStatsService.Instance.MoveTmpExpiditionInvToHub();
            }
            GameStatsService.Instance.gameStats.expeditionComplete = false;
            string txt = setupDay(GameStatsService.Instance.gameStats.daysLeft != 7);
            if (txt == null)
            {
                return;
            }
            startText += txt;
            blackoutTextBehaviour.WriteText(startText);
            consoleBehaviour.WriteText(startText);
        }
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
            restingCharacters.ForEach(c => {
                c.mentalHealth += 1;
                txt += c.name + " is going to rest." + "\n";
            });
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
        GameStatsService.Instance.gameStats.restingCharacters.ForEach(character => {
            character.mentalHealth += 1;
            tasksSummary += "\n" + character.name + " rested and is feeling a bit better.";
        });
        GameStatsService.Instance.gameStats.restingCharacters = null;
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
                    complication.on(task.doer);
                }
                if (result.bonus)
                {
                    IExtraAction bonus = HubBoons.getRandom(task.skillTest.skill);
                    tasksSummary += "\n" + bonus.GetMessage().Replace("{name}", task.doer.name);
                    bonus.on(task.doer);
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

    private string setupDay(bool newDay)
    {
        restingCharacters = new List<Character>();
        List<HubTask> tasks = new List<HubTask>();

        // If a character is damaged, add a damaged character task to do.
        Character dmgChar = GameStatsService.Instance.characters.Where(x => x.health <= 50).OrderBy(x => x.health).FirstOrDefault();
        if (dmgChar != null)
        {
            HubTask task = new HubTask();
            task.skillTest = new HealCharacterSkillTest(dmgChar);
            tasks.Add(task);
        }

        tasks.AddRange(HubTaskManager.getRandom(3, avalibleTasks.Select(x => x.skillTest).ToArray()));
        avalibleTasks = tasks.ToArray();

        string msg = "$ tasks -a \n TASKS requiring crew attention: \n ";
        for (var i = 0; i < avalibleTasks.Length; i++)
        {
            msg += avalibleTasks[i].skillTest.description + "\n ";
        }

        if (newDay)
        {
            string text;
            if(SubtractHungerAndTallyDead(out text))
            {
                GameOver(GameOverReason.DEATH);
                return null;
            }
            msg += text;
        }


        return msg;
    }


    private bool SubtractHungerAndTallyDead(out string text)
    {
        bool allDead = true;
        string msg = "";
        GameStatsService.Instance.characters.ToList().ForEach(character =>
        {
            character.hunger -= 1;
            if (!character.dead)
            {
                if (character.hunger <= 0)
                {
                    // Starved
                    character.dead = true;
                    character.deathReson = DeathReason.STARVATION;
                    msg += "\n" + TextConstants.STARVED.Replace("{name}", character.name);
                }
                if (character.health <= 0)
                {
                    // Just dead
                    character.dead = true;
                    character.deathReson = DeathReason.HEALTH;
                    msg += "\n" + TextConstants.DIED.Replace("{name}", character.name);
                }
                if (character.mentalHealth <= 0)
                {
                    // insane
                    character.dead = true;
                    character.deathReson = DeathReason.MENTAL_HEALTH;
                    msg += "\n" + TextConstants.INSANE.Replace("{name}", character.name);
                }
            }
            if (!character.dead)
            {
                allDead = false;
            }
        });

        text = msg;
        return allDead;
    }

    private void GameOver(GameOverReason reason)
    {
        if (reason == GameOverReason.TIMEOUT)
        {
            blackoutTextBehaviour.WriteText("Time has run out, and the window to contact earth has passed. By the next pass, the remaining crew will have starved.", false);
        } 
        else
        {
            string txt = "Everyone has died. \n";
            GameStatsService.Instance.characters.ToList().ForEach(character => {
                txt += character.name;
                switch(character.deathReson)
                {
                    case DeathReason.HEALTH:
                        txt += " health reached a critical condition.";
                        break;
                    case DeathReason.MENTAL_HEALTH:
                        txt += " couldn't take it anymore.";
                        break;
                    case DeathReason.STARVATION:
                        txt += " starved to death.";
                        break;
                    case DeathReason.EXPEDITION:
                        txt += " never came back from their expedtition.";
                        break;
                    default:
                        txt += " died of unkown causes.";
                        break;
                }
                txt += "\n";
            });
            blackoutTextBehaviour.WriteText(txt, false);
        }
    }

    private void Victory()
    {
        blackoutTextBehaviour.WriteText("Your signal reaches Earth. Hopefully rescue is swift.. The End.", false);
    }
}
