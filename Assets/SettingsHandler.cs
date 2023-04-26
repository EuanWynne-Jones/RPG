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

        [HideInInspector]
        [SerializeField] public bool e_HealthDisplay_OnHUD_OptionEnabled = true;
        [HideInInspector]
        [SerializeField] public bool e_HealthDisplay_OnCHAR_OptionEnabled = true;
        [HideInInspector]
        [SerializeField] public bool p_DamageNumbers_OptionEnabled = true;
        [HideInInspector]
        [SerializeField] public bool e_DamageNumbers_OptionEnabled = true;

        [SerializeField] public bool tutorial_OptionEnabled = true;



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
            e_HealthDisplay_OnHUD_OptionEnabled = true;
        }
        public void DisableEnemyHeathDisplay()
        {
            e_HealthDisplay_OnHUD_OptionEnabled = false;
        }

        public bool GetEnemyHealthDisplayStatus()
        {
            return e_HealthDisplay_OnHUD_OptionEnabled;
        }

        //EnemyHealthDisplay On CHAR Options
        public void EnableEnemyHeathOnCharDisplay()
        {
            e_HealthDisplay_OnCHAR_OptionEnabled = true;
        }
        public void DisableEnemyHeathOnCharDisplay()
        {
            e_HealthDisplay_OnCHAR_OptionEnabled = false;
        }

        public bool GetEnemyHealthOnCharDisplayStatus()
        {
            return e_HealthDisplay_OnCHAR_OptionEnabled;
        }

        //PlayerDamageNumbers options
        public void EnablePlayerDamageNumbers()
        {
            p_DamageNumbers_OptionEnabled = true;
        }
        public void DisablePlayerDamageNumbers()
        {
            p_DamageNumbers_OptionEnabled = false;
        }

        public bool GetPlayerDamageNumbersStatus()
        {
            return p_DamageNumbers_OptionEnabled;
        }

        //EnemyDamageNumbers options

        public void EnableEnemyDamageNumbers()
        {
            e_DamageNumbers_OptionEnabled = true;
        }
        public void DisableEnemyDamageNumbers()
        {
            e_DamageNumbers_OptionEnabled = false;
        }

        public bool GetEnemyDamageNumbersStatus()
        {
            return e_DamageNumbers_OptionEnabled;
        }

        //TutorialDisplay Options
        public void EnableTutorials()
        {
            tutorial_OptionEnabled = true;
        }
        public void DisableTutorials()
        {
            tutorial_OptionEnabled = false;
        }

        public bool GetTutorialsStatus()
        {
            return tutorial_OptionEnabled;
        }

    }
}
