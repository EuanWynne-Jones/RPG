using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RPG.UI;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        //[SerializeField] public TMP_Text healthText;
        [SerializeField] public Slider healthSlider;
        [SerializeField] public TextMeshProUGUI healthText;
        int healthIntValue;
        public PlayerSettings playerSettings;


        private void Awake()
        {
            health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            //old way showing 70%
            healthIntValue = (int)Math.Round(health.GetPercentage());
            //healthText.text = healthIntValue.ToString() + "%";
            healthSlider.value = healthIntValue;

            //showing 40/50 for health
            if (playerSettings.displayHealthOnPlayer)
            {
                healthText.gameObject.SetActive(true);
                healthText.text = Mathf.FloorToInt(health.GetHealth()) + "/" + health.GetMaxHealthBase();
            }
            if (!playerSettings.displayHealthOnPlayer) healthText.gameObject.SetActive(false); 
        }
    }
}
