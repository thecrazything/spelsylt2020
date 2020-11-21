using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class ExpeditionDefaultLoot
{
    static ExpeditionLevelState Storage = new ExpeditionLevelState
    {
        randomItems = new List<Item> {
            new Ration(),
            new Ration(),
            new Ration(),
            new Ration(),
            new Ration(),
            new Ration(),
        }
    };

    static ExpeditionLevelState Comms = new ExpeditionLevelState
    {
        randomItems = new List<Item> {
            new Ration(),
            new Ration(),
            new Ration(),
            new Ration(),
        }
    };

    static ExpeditionLevelState Admin = new ExpeditionLevelState
    {
        randomItems = new List<Item> {
            new Ration(),
            new Ration(),
            new Ration(),
            new Ration(),
            new Ration(),
            new Ration(),
        }
    };

    static ExpeditionLevelState PowerStation = new ExpeditionLevelState
    {
        randomItems = new List<Item> {
            new Ration(),
            new Ration(),
            new Ration(),
            new Ration(),
        }
    };

    public static Dictionary<string, ExpeditionLevelState> state = new Dictionary<string, ExpeditionLevelState>
    {
        { "Admin", Admin },
        { "Storage", Storage },
        { "Comms", Comms },
        { "PowerStation", PowerStation}
    };
}
