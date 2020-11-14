using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveConsequence : ChallengeConsequence
{
    public string successMessage;
    public string failMessage;

    public Transform target;

    protected override void consequence(SkillCheck.Result result, GameObject player)
    {
        if (result.success)
        {
            player.transform.position = target.position;
        }
    }
}
