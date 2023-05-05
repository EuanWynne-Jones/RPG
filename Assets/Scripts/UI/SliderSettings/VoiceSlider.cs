using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class VoiceSlider : SliderHandler
    {
        protected override float GetLastValue()
        {
            return playerSettings.voiceVolume;


        }
        protected override void SetSettings(float value)
        {
            playerSettings.voiceVolume = value;

        }
    }
}
