using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Stats 
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats stats;
        [SerializeField] TMP_Text levelText;

        private void Awake()
        {
            stats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {

            levelText.text = stats.CalculateLevel().ToString();
        }
    }

}
