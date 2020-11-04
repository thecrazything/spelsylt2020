﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataInjector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
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
        GameStatsService.Instance.SetStartData(characters, stats);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}