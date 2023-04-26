using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class ToggleHandler : MonoBehaviour
    {
        [SerializeField] Toggle toggleOption;
        public PlayerSettings playerSettings;

        private void Awake()
        {
            if(toggleOption == null)
            {
            toggleOption = GetComponent<Toggle>();
            }

            toggleOption.isOn = GetLastValue();
        }

        public void Activate()
        {
 
                SetSettings(toggleOption.isOn);
        }
        protected virtual void SetSettings(bool value)
        {

        }

        protected virtual bool GetLastValue()
        {
            return true;
        }
    }
}
