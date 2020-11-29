using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextConstants
{
    public static readonly string INTRO_MESSAGE = "$ diag --verbose \nCHECKING BASE INTEGRITY................................20% \n WARNING: Major atmosphere leaks detected \n HUB INTEGRITY.....................................90% \n WARNING: HUB Food rations LOW \n WARNING: Main Power Offline \n ERROR: Earth communications FAILED \n ERROR: Communications Offline \n Next communication window in...........................7 days";
    public static readonly string IDLE_TEXT = "$ Awaiting command";
    public static readonly string USER_DETAIL_NAME_TEXT = "$ status -n {name}";

    public static readonly string USER_HEALTH_5_TEXT = "{name} is healthy.";
    public static readonly string USER_HEALTH_4_TEXT = "{name}s' health has been better.";
    public static readonly string USER_HEALTH_3_TEXT = "{name} is not looking too good.";
    public static readonly string USER_HEALTH_2_TEXT = "{name} is badly hurt.";
    public static readonly string USER_HEALTH_1_TEXT = "{name} is near death.";

    public static readonly string STARVED = "{name} has starved to death.";
    public static readonly string DIED = "{name} has died.";
    public static readonly string INSANE = "{name} couldn't take it anymore.";

    public static readonly string DAYS_LEFT = "{days} days left";
    public static readonly string RATIONS_LEFT = "{count} Rations";

    public static readonly string NEXT_DAY_MESSAGE = "Preparing summary of previous day.......................................";

    public static readonly string ASSIGNED_TO = "Assigned to {name}.";
    public static readonly string EXPEDITION_ASSIGNED_TO = "Expedition assigned to {name}.";
    public static readonly string EXPEDITION_UNASSIGNED = "Expedition is unassigned.";

    public static readonly string PREPARE_EXPIDITION = "P R E P A R E  E X P E D I T I O N";
    public static readonly string START_EXPIDITION = "START EXPEDITION";
    public static readonly string NEXT_DAY = "N E X T  D A Y";
    public static readonly string SELECT_EXPEDITION = "S E L E C T  L O C A T I O N";

    public static readonly string START_NEW_DAY_QUESTION = "Start next day?";
    public static readonly string START_EXPIDITION_QUESTION = "Start expedition?";
    public static readonly string YES = "Y E S";

    public static readonly string[] RATION_FOUND_MESSAGES = new string[] { 
        "{name} found a ration!", 
        "{name} made a ration. Don't ask what's in it." 
    };
    public static readonly string[] RATION_LOST_MESSAGES = new string[] { 
        "{name} managed to lose a ration.", 
        "{name} accidentally threw a ration in the trash compactor." 
    };
    public static readonly string[] HEALTH_GAINED_MESSAGES = new string[] { 
        "{name} ate a pill they found and is feeling a bit healthier.", 
        "{name} walked off some of their pain." 
    };
    public static readonly string[] HEALTH_LOST_MESSAGES = new string[] { 
        "{name} cut themselves working.", 
        "A minor accident hurt {name}.", 
        "{name} put their limbs in the wrong place." 
    };
    public static readonly string[] MENTAL_HEALTH_GAINED_MESSAGES = new string[] {
        "Getting work done has cleared {name}s' mind."
    };
    public static readonly string[] MENTAL_HEALTH_LOST_MESSAGES = new string[] { 
        "Work took a toll on {name}s' mind.",
        "Too much work makes {name} dull.",
        "{Name} misses their family back home."
    };
}
