using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class EnemyHealthHUDToggle : ToggleHandler
    {
        protected override bool GetLastValue()
        {
            return playerSettings.displayEnemyHeathOnHUD;
        }

        protected override void SetSettings(bool value)
        {
            if(playerSettings != null)
            {
                playerSettings.displayEnemyHeathOnHUD = value;
            }
        }
    }
}
