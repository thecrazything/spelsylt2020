public class HealthLostAction : IExtraAction
{
    public string GetMessage()
    {
        return "{name} cut themselves working.";
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