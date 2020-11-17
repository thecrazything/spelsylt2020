﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutTextBehaviour : MonoBehaviour
{

    ConsoleBehaviour _console;
    public BlackoutBehaviour blackout;
    // Start is called before the first frame update
    void Start()
    {
        _console = GetComponent<ConsoleBehaviour>();
        _console.WriteText(TextConstants.INTRO_MESSAGE, (val) => {
            blackout.fade = true;
            return false;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}