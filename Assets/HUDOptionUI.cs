using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class HUDOptionUI : MonoBehaviour
    {
        [HideInInspector]
        [SerializeField] public bool activeHUD;

        private void Awake()
        {
        }
        public void SetActiveHUD(bool isActiveHuD)
        {
            activeHUD = isActiveHuD;
            HUDOptionUI[] hUDOptionUIs = FindObjectsOfType<HUDOptionUI>();
            foreach (HUDOptionUI hudOptionUI in hUDOptionUIs)
            {
                if (hudOptionUI != this) // if it's not the current HUDOptionUI
                {
                    hudOptionUI.activeHUD = false; // set activeHUD to false
                }
            }
        }
    }


}
