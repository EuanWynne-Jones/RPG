using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class AmbienceSlider : SliderHandler
    {
        protected override float GetLastValue()
        {
            return playerSettings.ambienceVolume;


        }
        protected override void SetSettings(float value)
        {
            playerSettings.ambienceVolume = value;

        }
    }
}
