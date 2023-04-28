using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class EnemyHeathNumbersToggle : ToggleHandler
    {
        protected override bool GetLastValue()
        {
            return playerSettings.displayHealthOnNPCS;
        }

        protected override void SetSettings(bool value)
        {
            if (playerSettings != null)
            {
                playerSettings.displayHealthOnNPCS = value;
            }
        }
    }

}