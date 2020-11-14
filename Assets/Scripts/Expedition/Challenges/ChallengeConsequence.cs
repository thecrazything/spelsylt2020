using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChallengeConsequence : MonoBehaviour
{
    public void onConsequence(SkillCheck.Result result, GameObject target)
    {
        Player player = target.GetComponent<Player>();
        consequence(result, target);

        if (result.complication)
        {
            ExpeditionComplications.getRandom(result.skill).on(player);
        }

        if (result.bonus)
        {
            ExpeditionBoons.getRandom(result.skill).on(player);
        }
    }

    protected abstract void consequence(SkillCheck.Result result, GameObject target);
}
