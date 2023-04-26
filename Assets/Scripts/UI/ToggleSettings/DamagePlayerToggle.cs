using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class DamagePlayerToggle : ToggleHandler
    {
        protected override bool GetLastValue()
        {
            return playerSettings.displayDamageOnPlayer;
        }

        protected override void SetSettings(bool value)
        {
            if (playerSettings != null)
            {
                playerSettings.displayDamageOnPlayer = value;
            }
        }
    }

}