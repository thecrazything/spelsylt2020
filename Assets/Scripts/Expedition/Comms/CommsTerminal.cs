using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CommsTerminal : MonoBehaviour, IInteractable
{
    public Light2D screenLight;

    public bool powered = false;
    public bool repaied = false;
    public bool activated = false;

    public float repairTime = 5f;
    public float activeTime = 5f;

    public float? GetActionTime(GameObject source)
    {
        // TODO check if player actually has the right items to repair, else null
        if (!powered)
        {
            return null;
        }
        if (!repaied)
        {
            return repairTime;
        }
        return activeTime;
    }

    public string GetActionTitle(GameObject source)
    {
        if (repaied && powered && activated)
        {
            return null;
        }
        // TODO check if player actually has the right items to repair, else null
        if (!powered)
        {
            return null;
        }
        if (!repaied)
        {
            return "Repairing....";
        }
        return "Activating comms....";
    }

    public void Interact(GameObject source)
    {
        Player player = source.GetComponent<Player>();
        if (!powered)
        {
            player.console.ShowMessage("The comms station has no power.");
            return;
        }

        if (!repaied)
        {
            // TODO check if player has repair parts. Use and remove the item.
            bool hasParts = true;
            if (hasParts)
            {
                player.console.ShowMessage("The comms are now working. I should be able to contact Earth now!");
                repaied = true;
                return;
            }
            else
            {
                player.console.ShowMessage("There is power, but the comms seem broken. I need to find some spare parts.");
                return;
            }
        }

        if (repaied && powered)
        {
            activated = true;
            GameStatsService.Instance.gameStats.victoryCondition = true;
            player.console.ShowMessage("The comms are up and running, broadcasting to earth.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        powered = GameStatsService.Instance.gameStats.isPowerOn;
        screenLight.enabled = powered;
        activated = GameStatsService.Instance.gameStats.victoryCondition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
