using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class PlayerHeathNumbersToggle : ToggleHandler
    {
        protected override bool GetLastValue()
        {
            return playerSettings.displayHealthOnPlayer;
        }

        protected override void SetSettings(bool value)
        {
            if (playerSettings != null)
            {
                playerSettings.displayHealthOnPlayer = value;
            }
        }
    }
}
