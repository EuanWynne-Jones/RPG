using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class IconsToggle : ToggleHandler
    {
        protected override bool GetLastValue()
        {
            return playerSettings.displayTooltipIcons;
        }

        protected override void SetSettings(bool value)
        {
            if (playerSettings != null)
            {
                playerSettings.displayTooltipIcons = value;
            }
        }
    }
}
