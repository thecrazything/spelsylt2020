using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataInjector : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (GameStatsService.Instance.characters == null && GameStatsService.Instance.gameStats == null)
        {
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

            if (GameStatsService.Instance.selectedCharacter == null)
            {
                GameStatsService.Instance.selectedCharacter = a;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
