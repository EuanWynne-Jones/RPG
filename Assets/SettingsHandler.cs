using RPG.UI.Inventories;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class SettingsHandler : MonoBehaviour
    {
        [Header("Options")]
        [SerializeField] public bool iconOptionEnabled = true;

        [SerializeField] public bool E_HealthDisplay_OnHUD_OptionEnabled = true;
        [SerializeField] public bool E_HealthDisplay_OnCHAR_OptionEnabled = true;


        //Icon Options
        public void EnableIconOption()
        {
            iconOptionEnabled = true;
        }

        public void DisableIconOption()
        {
            iconOptionEnabled = false;
        }

        public bool GetIconOptionStatus()
        {
            return iconOptionEnabled;
        }


        //EnemyHealthDisplay On HUD Options
        public void EnableEnemyHeathDisplay()
        {
            E_HealthDisplay_OnHUD_OptionEnabled = true;
        }
        public void DisableEnemyHeathDisplay()
        {
            E_HealthDisplay_OnHUD_OptionEnabled = false;
        }

        public bool GetEnemyHealthDisplayStatus()
        {
            return E_HealthDisplay_OnHUD_OptionEnabled;
        }

        //EnemyHealthDisplay On CHAR Options
        public void EnableEnemyHeathOnCharDisplay()
        {
            E_HealthDisplay_OnCHAR_OptionEnabled = true;
        }
        public void DisableEnemyHeathOnCharDisplay()
        {
            E_HealthDisplay_OnCHAR_OptionEnabled = false;
        }

        public bool GetEnemyHealthOnCharDisplayStatus()
        {
            return E_HealthDisplay_OnCHAR_OptionEnabled;
        }
    }
}
