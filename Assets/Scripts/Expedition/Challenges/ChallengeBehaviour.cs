using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class ChallengeBehaviour : MonoBehaviour, IInteractable
{
    public SkillsEnum skill;
    public int difficulty;
    public string testName;
    public string description;

    public float executeTime;

    private SkillTest _skillTest;
    private ChallengeConsequence _consequence;

    public void Interact(GameObject source)
    {
        if (source == null)
        {
            throw new ArgumentNullException("Tried to interact will null");
        }
        Player player = source.GetComponent<Player>();
        if (player == null)
        {
            throw new ArgumentNullException("No player component on interaction source");
        }

        var result = SkillCheck.DoCheck(player.character, _skillTest);
        if (_consequence != null)
        {
            _consequence.onConsequence(result, source);
        }
        else
        {
            Debug.Log("No consequences found, but result was " + (result.success ? "success" : "fail"));
        }
    }

    public float? GetActionTime(GameObject source)
    {
        return executeTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        _skillTest = new SkillTest(skill, difficulty);
        _consequence = GetComponent<ChallengeConsequence>();

        if (_consequence == null)
        {
            Debug.LogWarning("No consequence provided, challenge will have no effect.");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetActionTitle(GameObject source)
    {
        return null;
    }
}
