﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStatsService
{
    private static GameStatsService _serviceInstance;
    private ICollection<Character> _characters;
    public GameStats gameStats;
    public ICollection<Character> characters
    {
        get
        {
            return _characters;
        }
    }
    public Character selectedCharacter;

    public static GameStatsService Instance
    {
        get
        {
            if (_serviceInstance == null)
            {
                _serviceInstance = new GameStatsService();
            }
            return _serviceInstance;
        }
    }

    public Character GetCharacterById(int id)
    {
        return _characters.FirstOrDefault(c => c.id == id);
    }
}
