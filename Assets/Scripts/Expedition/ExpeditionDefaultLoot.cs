using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class ExpeditionDefaultLoot
{
    public static ExpeditionLevelState state = new ExpeditionLevelState
    {
        randomItems = new List<Item> {
            new Ration(),
            new Ration(),
            new Ration(),
            new Ration(),
            new Ration(),
        }
    };
}
