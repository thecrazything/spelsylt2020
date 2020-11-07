using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextConstants
{
    public static readonly string INTRO_MESSAGE = "$ Check base integrity \nCHECKING BASE INTEGRITY................................20% \n WARNING: Major atomosphere leaks detected \n HUB INTEGRITY.....................................90% \n WARNING: HUB Food rations LOW \n ERROR: Earth communications FAILED \n ERROR: Communications Offline \n Next communication window in..................................12 days";
    public static readonly string IDLE_TEXT = "$ Awaiting command";
    public static readonly string USER_DETAIL_NAME_TEXT = "$ status -n {name}";

    public static readonly string USER_HEALTH_5_TEXT = "{name} is feeling good.";
    public static readonly string USER_HEALTH_4_TEXT = "{name} has been better.";
    public static readonly string USER_HEALTH_3_TEXT = "{name} is not looking too good.";
    public static readonly string USER_HEALTH_2_TEXT = "{name} is badly hurt.";
    public static readonly string USER_HEALTH_1_TEXT = "{name} is near death.";

    public static readonly string DAYS_LEFT = "{days} days left";

    public static readonly string NEXT_DAY_MESSAGE = "Preparing summary of previous day.......................................";

    public static readonly string ASSIGNED_TO = "Assigned to {name}.";
    public static readonly string EXPEDITION_ASSIGNED_TO = "Expedition assigned to {name}.";
    public static readonly string EXPEDITION_UNASSIGNED = "Expedition is unassigned.";
}
