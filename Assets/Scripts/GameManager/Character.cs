using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public delegate void CharacterStatChangeDelegate(Character stats);
    public event CharacterStatChangeDelegate onCharacterChange;

    private int _id = 0;
    private string _name = "TESTNAME";
    private float _health;
    private float _maxHealth = 100f;
    private int _hunger = 3;
    private int _mentalHealth = 5;
    private int _mentalHealthMax = 10;

    public SkillsEnum skill { get; private set; }

    public int id
    {
        get
        {
            return _id;
        }
    }

    public string name
    {
        get
        {
            return _name;
        }
    }

    public float health
    {
        get
        {
            return _health;
        }
    }

    public bool dead = false;
    public DeathReason deathReson;

    public int hunger
    {
        get
        {
            return _hunger;
        }
        set
        {
            _hunger = value;
            if (_hunger > 6)
            {
                _hunger = 6;
            }
        }
    }

    public int mentalHealth
    {
        get
        {
            return _mentalHealth;
        }
        set
        {
            _mentalHealth = value;
            if (_mentalHealth > 6)
            {
                _mentalHealth = 6;
            }

        }
    }

    public Character(int id, string name, SkillsEnum skill)
    {
        _id = id;
        _name = name;
        _health = _maxHealth;
        this.skill = skill;
    }

    public void subtractHealth(float value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Cannot subract a negative value. Use addHealth instead.");
        }
        _health -= value;
        if (_health < 0)
        {
            _health = 0;
        }
        onCharacterChange?.Invoke(this);
    }
    public void addHealth(float value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Cannot add a negative value. Use subctractHealth instead.");
        }
        _health += value;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        onCharacterChange?.Invoke(this);
    }
}
