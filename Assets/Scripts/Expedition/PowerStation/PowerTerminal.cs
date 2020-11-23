using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerTerminal : MonoBehaviour, IInteractable
{
    public float powerOnTime = 5f;

    public bool powered = false;

    public float? GetActionTime(GameObject source)
    {
        return powered ? null : (float?) powerOnTime;
    }

    public string GetActionTitle(GameObject source)
    {
        return powered ? null : "Reactivating....";
    }

    public void Interact(GameObject source)
    {
        Player player = source.gameObject.GetComponent<Player>();
        if (powered)
        {
            player.console.ShowMessage("The power is on.");
        }
        else
        {
            powered = true;
            GameStatsService.Instance.gameStats.isPowerOn = true;
            player.console.ShowMessage("The station power has now been turned on again.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        powered = GameStatsService.Instance.gameStats.isPowerOn;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
