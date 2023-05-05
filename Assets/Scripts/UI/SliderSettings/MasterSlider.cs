using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class MasterSlider : SliderHandler
    {
        
        protected override float GetLastValue()
        {
            return playerSettings.masterVolume;


        }
        protected override void SetSettings(float value)
        {
            playerSettings.masterVolume = value;
            
        }
    }
}
