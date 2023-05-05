using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class UISlider : SliderHandler
    {
        protected override float GetLastValue()
        {
            return playerSettings.interfaceVolume;


        }
        protected override void SetSettings(float value)
        {
            playerSettings.interfaceVolume = value;

        }
    }
}
