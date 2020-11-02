using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private string _name = "TESTNAME";
    private float _health;
    private float _maxHealth = 100f;

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

    public void subtractHealth(float value)
    {
        if (value < 0 )
        {
            throw new ArgumentException("Cannot subract a negative value. Use addHealth instead.")
        }
        _health -= value;
        if (_health < 0)
        {
            _health = 0;
        }


        public void addHealth(float value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Cannot add a negative value. Use subctractHealth instead.")
            }
            _health += value;
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
        }
    }
