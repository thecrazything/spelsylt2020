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
    private bool _dead = false;
    private int _hunger = 2;

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

    public bool dead
    {
        get
        {
            return _dead;
        }
        set
        {
            _dead = value;
        }
    }


    public int hunger
    {
        get
        {
            return _hunger;
        }
        set
        {
            _hunger = value;
            if (_hunger <= 0)
            {
                dead = true;
            }
            else if (_hunger > 6)
            {
                _hunger = 6;
            }
        }
    }

    public Character(int id, string name)
    {
        _id = id;
        _name = name;
        _health = _maxHealth;
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
