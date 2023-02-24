using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Attributes;


namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        [SerializeField] public TMP_Text healthText;
        int healthIntValue;

        private void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if(fighter.GetTarget() == null)
            {
                healthText.text = "N/A";
                return;
            }
            Health health = fighter.GetTarget();
            //old way showing 70%
            healthIntValue = (int)Math.Round(health.GetPercentage());
            healthText.text = healthIntValue.ToString() + "%";
            healthText.text = String.Format("{0:0}%", health.GetPercentage());

            // showing 40/50 for health
            //healthText.GetComponent<TMP_Text>().text = String.Format("{0:0}/{1:00}", health.GetHealth(), health.GetMaxHealth());
        }
    }
}