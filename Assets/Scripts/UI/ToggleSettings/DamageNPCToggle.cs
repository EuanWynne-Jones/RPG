using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class DamageNPCToggle : ToggleHandler
    {
        protected override bool GetLastValue()
        {
            return playerSettings.displayDamageOnNPCS;
        }

        protected override void SetSettings(bool value)
        {
            if (playerSettings != null)
            {
                playerSettings.displayDamageOnNPCS = value;
            }
        }
    }
}
