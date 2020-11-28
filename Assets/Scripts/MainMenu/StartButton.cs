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

        Character a = new Character(0, "Sam Stevens", SkillsEnum.LEADERSHIP);
        characters.Add(a);
        Character b = new Character(1, "Charlie Smith", SkillsEnum.MECHANIC);
        characters.Add(b);
        Character c = new Character(2, "Frankie Thorn", SkillsEnum.MEDIC);
        characters.Add(c);
        Character d = new Character(3, "Jayden Wynn", SkillsEnum.UTILITY);
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
}
