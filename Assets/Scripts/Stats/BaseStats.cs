using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GameDevTV.Utils;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,10)]
        [SerializeField] int startingLevel = 1;


        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpEffect = null;
        [SerializeField] bool useModifiers = false;

        LazyValue<int> currentLevel;
        public event Action onLevelUp;
        Experience experience;

        private void Awake()
        {
            currentLevel = new LazyValue<int>(CalculateLevel);
            experience = GetComponent<Experience>();
        }

        private void Start()
        {
            currentLevel.ForceInit();
        }


        private void OnEnable()
        {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void OnDisable()
        {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                LevelUpEffect();
                onLevelUp();
            }
        }



        private void LevelUpEffect()
        {
            Instantiate(levelUpEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return GetBaseStat(stat) + GetAdditiveModifier(stat) * (1 + GetPersentageModifier(stat) / 100);
        }



        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        private float GetAdditiveModifier(Stat stat)
        {
            if (!useModifiers) return 0f;
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPersentageModifier(Stat stat)
        {
            if (!useModifiers) return 0f;
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        public int GetLevel()
        {
            return currentLevel.value;
        }

        

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currentEXP = experience.experiencePoints;
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float EXPtoLevelUP = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);
                if(EXPtoLevelUP > currentEXP)
                {
                    return level;
                }
            }
            return penultimateLevel + 1;
        }

    }
}