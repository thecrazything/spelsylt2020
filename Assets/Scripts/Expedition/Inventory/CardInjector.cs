using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInjector : LootInjector
{
    public KeycardColor color;

    // Start is called before the first frame update
    void Start()
    {
        items = new Item[] { new Keycard(color) };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
