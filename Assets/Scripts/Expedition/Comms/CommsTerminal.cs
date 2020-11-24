using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private AudioSource _sound;

    public AudioClip GetActionSound(GameObject source)
    {
        return null;
    }

    public float? GetActionTime(GameObject source)
    {
        Player player = source.GetComponent<Player>();
        CommsParts parts = GetParts(player);
        if (!powered)
        {
            return null;
        }
        if (!repaied)
        {
            return parts == null ? null : (float?)repairTime;
        }
        return activeTime;
    }

    public string GetActionTitle(GameObject source)
    {
        Player player = source.GetComponent<Player>();
        CommsParts parts = GetParts(player);
        if (repaied && powered && activated)
        {
            return null;
        }
        if (!powered)
        {
            return null;
        }
        if (!repaied)
        {
            return parts == null ? null : "Repairing....";
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
            CommsParts parts = GetParts(player);
            if (parts != null)
            {
                player.console.ShowMessage("The comms are now working. I should be able to contact Earth now!");
                repaied = true;
                activated = true;
                GameStatsService.Instance.gameStats.victoryCondition = true;
                _sound.Play();
                player.inventory.GetInventory().Remove(parts);
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
            player.console.ShowMessage("The comms are up and running, broadcasting to earth.");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        powered = GameStatsService.Instance.gameStats.isPowerOn;
        screenLight.enabled = powered;
        activated = GameStatsService.Instance.gameStats.victoryCondition;
        _sound = GetComponent<AudioSource>();

        if (activated)
        {
            _sound.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private CommsParts GetParts(Player player)
    {
        return player.inventory.GetInventory().Where(x => x is CommsParts).Cast<CommsParts>().FirstOrDefault();
    }
}
