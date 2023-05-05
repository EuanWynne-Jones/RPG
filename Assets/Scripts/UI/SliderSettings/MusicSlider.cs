using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class MusicSlider : SliderHandler
{
        protected override float GetLastValue()
        {
            return playerSettings.soundtrackVolume;


        }
        protected override void SetSettings(float value)
        {
            playerSettings.soundtrackVolume = value;

        }
    }
}
