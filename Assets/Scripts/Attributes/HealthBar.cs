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

        private void Update()
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