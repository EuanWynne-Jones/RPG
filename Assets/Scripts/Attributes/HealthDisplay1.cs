using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplayEnemies : MonoBehaviour
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
            healthIntValue = (int)Math.Round(health.GetPercentage());
            healthText.text = healthIntValue.ToString() + "%";
        }
    }
}
