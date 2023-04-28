using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Attributes;
using UnityEngine.UI;
using RPG.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        //[SerializeField] public TMP_Text healthText;
        [SerializeField] public Slider healthSlider;
        [SerializeField] public TextMeshProUGUI healthText;
        GameObject healthSliderGO = null;
        int healthIntValue;
        [SerializeField] PlayerSettings playerSettings;

        //[SerializeField] string ID;



        private void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
            UpdateHealthDisplayUI();

            //UIEvents uieve = FindObjectOfType<UIEvents>();
            //if (uieve != null)
            //    uieve.MyEvent += MyEventListener;


        }

        public void UpdateHealthDisplayUI()
        {
            healthSliderGO = FindObjectOfType<EnemyHealthUI>().gameObject;
            healthSlider = healthSliderGO.GetComponent<Slider>();
            healthText = healthSliderGO.GetComponentInChildren<TextMeshProUGUI>();
        }

        //private void OnDestroy()
        //{
        //    UIEvents uieve = FindObjectOfType<UIEvents>();
        //    uieve.MyEvent -= MyEventListener;
        //}

        //void MyEventListener(string inputID)
        //{
        //    if(inputID == ID)
        //        Debug.Log(this.name + " " + inputID);
        //}

        private void Start()
        {
            healthSliderGO.SetActive(false);
        }

        private void Update()
        {
            if (!playerSettings.displayEnemyHeathOnHUD)
            {
                healthSliderGO.SetActive(false);
                return;
            }
            
                if (fighter.GetTarget() == null)
            {
                healthSliderGO.SetActive(false);
                return;
            }

                Health health = fighter.GetTarget();
                healthSliderGO.SetActive(true);
                healthIntValue = (int)Math.Round(health.GetPercentage());
                healthSlider.value = healthIntValue;
                healthText.text =Mathf.FloorToInt(health.GetHealth()) + "/" + health.GetMaxHealthBase();

                if(fighter.GetTarget().GetComponent<Health>().isDead == true)
                {
                    healthSliderGO.SetActive(false);
                }

           
                // showing 40/50 for health
            //healthText.text = healthIntValue.ToString() + "%";
            //healthText.text = String.Format("{0:0}%", health.GetPercentage());
        }
    }
}