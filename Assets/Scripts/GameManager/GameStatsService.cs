﻿using System;
using System.Collections.Generic;
using System.Linq;

public class GameStatsService
{
    public delegate void ChangeSelectedCharacterEvent(Character character);
    public event ChangeSelectedCharacterEvent onChangeSelectedCharacter;

    private static GameStatsService _serviceInstance;
    private ICollection<Character> _characters;
    private Character _selectedCharacter;
    public GameStats gameStats;

    public ICollection<Character> characters
    {
        get
        {
            return _characters;
        }
    }
    public Character selectedCharacter { 
        get
        {
            return _selectedCharacter;
        }
        set {
            _selectedCharacter = value;
            onChangeSelectedCharacter?.Invoke(_selectedCharacter);
        } 
    }

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
        if (_characters == null)
        {
            throw new ArgumentNullException("Character list was null.");
        }
        return _characters.FirstOrDefault(c => c.id == id);
    }

    public void CompleteExpedition(Item[] items)
    {
        if (items != null)
        {
            // TODO handle other item types
            items.Where(x => x is Ration).ToList().ForEach(item =>
            {
                gameStats.AddItem(item as Ration);
            });
        }
        gameStats.expeditionComplete = true;
    }

    public void SetStartData(ICollection<Character> characters, GameStats gameStats)
    {
        _characters = characters;
        this.gameStats = gameStats;
    }
}
