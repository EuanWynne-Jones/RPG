using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        //[SerializeField] public TMP_Text experienceText;
        [SerializeField] public Slider experienceSlider;

        private void Awake()
        {
            experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            
            //experienceText.text = experience.experiencePoints.ToString();
            experienceSlider.value = experience.GetPercentage();
        }
    }
}
