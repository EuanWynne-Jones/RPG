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
        [SerializeField] public TextMeshProUGUI experienceText;

        private BaseStats baseStats;

        private void Awake()
        {
            experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
            baseStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {

            experienceText.text = experience.experiencePoints.ToString() + "/ " + baseStats.GetStat(Stat.ExperienceToLevelUp).ToString() + " ("+ experience.GetPercentRemaining().ToString() + "%)";
            experienceSlider.value = experience.GetPercentRemaining();
        }
    }
}
