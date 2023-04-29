using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{

    public class SliderHandler : MonoBehaviour
    {
        [SerializeField] public Slider slider;
        [SerializeField] float slidervalue;
        [SerializeField] public PlayerSettings playerSettings;
        [SerializeField] public VolumeSetter volumeSetter;

        private void Awake()
        {
            if(slider == null)
            {
            slider = GetComponent<Slider>();
            }
            slidervalue = GetLastValue();
            slider.value = slidervalue;

        }
        public void Activate()
        {
            SetSettings(slider.value);
            slidervalue = slider.value;
            volumeSetter.setVolumes();
            playerSettings.Save();
        }
        protected virtual void SetSettings(float value)
        {

        }

        protected virtual float GetLastValue()
        {
            return slidervalue;
        }
    }
}
