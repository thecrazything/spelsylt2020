using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    private void NewGame()
    {
        var statsService = GameStatsService.Instance;
        List<Character> characters = new List<Character>();

        Character a = NewCharacter(0, "Sam Stevens", SkillsEnum.LEADERSHIP);
        characters.Add(a);
        Character b = NewCharacter(1, "Charlie Smith", SkillsEnum.MECHANIC);
        characters.Add(b);
        Character c = NewCharacter(2, "Frankie Thorn", SkillsEnum.MEDIC);
        characters.Add(c);
        Character d = NewCharacter(3, "Jayden Wynn", SkillsEnum.UTILITY);
        characters.Add(d);
        GameStats stats = new GameStats();
        GameStatsService.Instance.SetStartData(characters, stats);
        statsService.SetStartData(characters, stats);
    }
    public void onClick()
    {
        NewGame();
        SceneManager.LoadScene("Hub");
    }

    private int GetStartHunger()
    {
        return Random.Range(2, 5);
    }

    private Character NewCharacter(int id, string name, SkillsEnum skill)
    {
        Character a = new Character(id, name, skill);
        a.hunger = GetStartHunger();
        return a;
    }
}
