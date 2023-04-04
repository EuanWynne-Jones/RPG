using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Attributes;
using UnityEngine.UI;


namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        //[SerializeField] public TMP_Text healthText;
        [SerializeField] public Slider healthSlider;
        private GameObject healthSliderGO;
        int healthIntValue;

        private void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
            healthSliderGO = healthSlider.gameObject;
            
        }
        private void Start()
        {
            healthSliderGO.SetActive(false);
        }

        private void Update()
        {
            if(fighter.GetTarget() == null)
            {
                //healthText.text = "N/A";
                healthSliderGO.SetActive(false);
                return;
            }
            Health health = fighter.GetTarget();
            healthSliderGO.SetActive(true);
            healthIntValue = (int)Math.Round(health.GetPercentage());
            healthSlider.value = healthIntValue;

            //healthText.text = healthIntValue.ToString() + "%";
            //healthText.text = String.Format("{0:0}%", health.GetPercentage());
            if(fighter.GetTarget().GetComponent<Health>().isDead == true)
            {
                healthSliderGO.SetActive(false);
            }
            // showing 40/50 for health
            //healthText.GetComponent<TMP_Text>().text = String.Format("{0:0}/{1:00}", health.GetHealth(), health.GetMaxHealth());
        }
    }
}