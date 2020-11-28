using UnityEngine;

public class HealthGainedAction : IExtraAction
{
    public string GetMessage()
    {
        return TextConstants.HEALTH_GAINED_MESSAGES[Random.Range(0, TextConstants.HEALTH_GAINED_MESSAGES.Length)];
    }

    public SkillsEnum GetSkill()
    {
        return SkillsEnum.NEUTRAL;
    }

    public void on(Character stats)
    {
        stats.addHealth(10);
    }

    public void on(Player character)
    {
        throw new System.NotImplementedException();
    }
}