using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        [SerializeField] public TMP_Text healthText;
        int healthIntValue;

        private void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            //old way showing 70%
            healthIntValue = (int)Math.Round(health.GetPercentage());
            healthText.text = healthIntValue.ToString() + "%";

            //showing 40/50 for health
            //healthText.GetComponent<TMP_Text>().text = String.Format("{0:0}/{1:00}", health.GetHealth(), health.GetMaxHealth());
        }
    }
}
