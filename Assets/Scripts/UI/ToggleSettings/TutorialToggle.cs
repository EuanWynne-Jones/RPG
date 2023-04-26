using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class TutorialToggle : ToggleHandler
    {
        protected override bool GetLastValue()
        {
            return playerSettings.displayTutorials;
        }

        protected override void SetSettings(bool value)
        {
            if (playerSettings != null)
            {
                playerSettings.displayTutorials = value;
            }
        }
    }
}
