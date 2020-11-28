using UnityEngine;

public class MentalHealthLostAction : IExtraAction
{
    public string GetMessage()
    {
        return TextConstants.MENTAL_HEALTH_LOST_MESSAGES[Random.Range(0, TextConstants.MENTAL_HEALTH_LOST_MESSAGES.Length)];
    }

    public SkillsEnum GetSkill()
    {
        return SkillsEnum.NEUTRAL;
    }

    public void on(Character stats)
    {
        stats.mentalHealth -= 1;
    }

    public void on(Player character)
    {
        throw new System.NotImplementedException();
    }
}