using RPG.Core.UI.Tooltips;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class QuestTooltipSpawner : TooltipSpawner
    {
        public override bool CanCreateTooltip()
        {
            return true;
        }

        public override void UpdateTooltip(GameObject tooltip)
        {
        }


    }
}
