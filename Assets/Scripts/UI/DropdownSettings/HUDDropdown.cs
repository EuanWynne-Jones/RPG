using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class HUDDropdown : DropdownHandler
    {

        [SerializeField] HUDToggle hudToggle;


        protected override int GetLastValue()
        {
            return (int)playerSettings.hudDisplayOption;
        }

        protected override void SetSettings(int value)
        {
            if (playerSettings != null)
            {
                playerSettings.hudDisplayOption = (PlayerSettings.HUDdisplayOptions)value;
                hudToggle.SetUI();

            }

        }
    }
}
