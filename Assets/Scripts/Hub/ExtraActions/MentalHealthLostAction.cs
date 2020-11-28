public class MentalHealthLostAction : IExtraAction
{
    public string GetMessage()
    {
        return "Work took a tool on {name}s' mind.";
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