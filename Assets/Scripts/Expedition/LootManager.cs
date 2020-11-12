using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LootManager : MonoBehaviour
{
    public int RationsPool;

    LootContainer[] containers;

    void Start()
    {
        containers = GameObject.FindGameObjectsWithTag("LootContainer")
            .Select(i => i.GetComponent<LootContainer>())
            .ToArray();
        Debug.Log(containers.Length);
        for (int i = 0; i < RationsPool; i++)
        {
            GetRandomContainer().inventory.Add(new Ration());
        }
    }

    LootContainer GetRandomContainer()
    {
        int randomNumber = Random.Range(0, containers.Length);
        return containers[randomNumber];
    }
}
