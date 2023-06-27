using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] public float experiencePoints = 0;
        public float StoredExperiencePoints = 0;

        public event Action onExperienceGained;
        BaseStats baseStats;

        private void Awake()
        {
            baseStats = GetComponent<BaseStats>();
        }

        private void Update()
        {
            //Debug.Log("CurrentEXP: " + experiencePoints);
            UpdateStoredEXP();
        }

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            //GetNumberRemaining(experiencePoints);

            
            if (experiencePoints > baseStats.GetStat(Stat.ExperienceToLevelUp))
            {
                GetNumberRemaining(experiencePoints);
                //Debug.Log("StoredEXP: " + StoredExperiencePoints);
            }

            onExperienceGained();
        }

        private void UpdateStoredEXP()
        {
            if (StoredExperiencePoints > 0)
            {
                float ExperienceToGainAfterLevel = GetNumberRemaining(StoredExperiencePoints);
                GainExperienceOnLevelup(ExperienceToGainAfterLevel);
                
            }
        }

        public void GainExperienceOnLevelup(float ExperienceToGainAfterLevel)
        {
            experiencePoints += ExperienceToGainAfterLevel;
            onExperienceGained();
        }
        public float GetPercentage()
        {
            return 100 * GetFraction();
        }
        public float GetFraction()
        {
            return experiencePoints / baseStats.GetStat(Stat.ExperienceToLevelUp);
        }

        public float GetNumberRemaining(float experience)
        {
            if (experience > baseStats.GetStat(Stat.ExperienceToLevelUp))
            {
                return StoredExperiencePoints = Math.Abs(experience - baseStats.GetStat(Stat.ExperienceToLevelUp));
            }
            else
            {
               return StoredExperiencePoints = 0f;
            }
        }
        public float GetPercentRemaining()
        {
            float percentageToLevelUp = ((float)experiencePoints / baseStats.GetStat(Stat.ExperienceToLevelUp) * 100f);
            return percentageToLevelUp; 
        }
        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }
}

