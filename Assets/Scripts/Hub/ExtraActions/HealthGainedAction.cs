public class HealthGainedAction : IExtraAction
{
    public string GetMessage()
    {
        return "{name} ate a pill they found and is feeling a bit healthier.";
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