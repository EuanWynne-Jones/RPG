using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class EffectsSlider : SliderHandler
    {

        protected override float GetLastValue()
        {
            return playerSettings.effectsVolume;


        }
        protected override void SetSettings(float value)
        {
            playerSettings.effectsVolume = value;

        }
    }
}
