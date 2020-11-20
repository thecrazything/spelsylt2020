using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterTextFormatter
{

    public static string FormatSkill(Character character)
    {
        switch(character.skill)
        {
            case SkillsEnum.LEADERSHIP:
                return "Leader";
            case SkillsEnum.MECHANIC:
                return "Mechanic";
            case SkillsEnum.MEDIC:
                return "Medic";
            case SkillsEnum.UTILITY:
                return "Utility";
            default:
                return "";
        }
    }

    public static string FormatHunger(Character character)
    {
        if (character.hunger == 1)
        {
            return character.name + " is starving.";
        }
        else if (character.hunger == 2)
        {
            return character.name + " is very hungry.";
        }
        else if (character.hunger == 3)
        {
            return character.name + " is hungry.";
        }
        else if (character.hunger == 4)
        {
            return character.name + " could eat.";
        }
        else
        {
            return character.name + " is full.";
        }
    }

    public static string FormatMentalHealth(Character character)
    {
        if (character.mentalHealth <= 2)
        {
            return character.name + " is loosing it.";
        }
        else if (character.mentalHealth <= 4)
        {
            return character.name + " is not in a good place.";
        }
        else if (character.mentalHealth <= 6)
        {
            return character.name + " is fine.";
        }
        else if (character.mentalHealth <= 8)
        {
            return character.name + " is happy.";
        }
        else
        {
            return character.name + " is at peak mental capacity";
        }
    }

    public static string FormatHealth(Character character)
    {
        if (character.health <= 20)
        {
            return TextConstants.USER_HEALTH_1_TEXT;
        }
        else if(character.health <= 40)
        {
            return TextConstants.USER_HEALTH_2_TEXT;
        }
        else if (character.health <= 60)
        {
            return TextConstants.USER_HEALTH_3_TEXT;
        }
        else if (character.health <= 80)
        {
            return TextConstants.USER_HEALTH_4_TEXT;
        }
        else
        {
            return TextConstants.USER_HEALTH_5_TEXT;
        }
    }
}
