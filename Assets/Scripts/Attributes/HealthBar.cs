using RPG.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health health = null;
        [SerializeField] RectTransform healthbar = null;
        [SerializeField] Canvas healthbarRoot = null;
        SettingsHandler settingsHandler;

        private void OnEnable()
        {
            settingsHandler = FindObjectOfType<SettingsHandler>();
        }
        private void Update()
        {
            if(settingsHandler == null) settingsHandler = FindObjectOfType<SettingsHandler>();
            if (settingsHandler.GetEnemyHealthOnCharDisplayStatus() == false)
            {
                healthbarRoot.enabled = false;
                return;
            }
            else
            {
                if (Mathf.Approximately(health.GetFraction(), 0)
                || Mathf.Approximately(health.GetFraction(), 1))
                {
                    healthbarRoot.enabled = false;
                    return;
                }

                healthbarRoot.enabled = true;
                healthbar.localScale = new Vector3(health.GetFraction(), 1, 1);

            }
        }

       
    }

    
}