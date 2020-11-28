using UnityEngine;

public class HealthLostAction : IExtraAction
{
    public string GetMessage()
    {
        return TextConstants.HEALTH_LOST_MESSAGES[Random.Range(0, TextConstants.HEALTH_LOST_MESSAGES.Length)];
    }

    public SkillsEnum GetSkill()
    {
        return SkillsEnum.NEUTRAL;
    }

    public void on(Character stats)
    {
        stats.subtractHealth(10);
    }

    public void on(Player character)
    {
        throw new System.NotImplementedException();
    }
}