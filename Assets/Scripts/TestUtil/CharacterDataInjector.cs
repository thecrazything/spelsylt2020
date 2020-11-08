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

            Character a = new Character(0, "Steve McQueen");
            characters.Add(a);
            Character b = new Character(1, "Rachel Rocket");
            characters.Add(b);
            Character c = new Character(2, "Peggy Hunter");
            characters.Add(c);
            Character d = new Character(3, "Micheal Moore");
            characters.Add(d);
            GameStats stats = new GameStats();
            stats.AddRation(new RationItem());
            stats.AddRation(new RationItem());
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
