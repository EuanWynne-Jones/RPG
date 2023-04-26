using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class EnemyHealthCHARToggle : ToggleHandler
    {
        protected override bool GetLastValue()
        {
            return playerSettings.displayEnemyHeathOnCharacter;
        }

        protected override void SetSettings(bool value)
        {
            if (playerSettings != null)
            {
                playerSettings.displayEnemyHeathOnCharacter = value;
            }
        }
    }
}
