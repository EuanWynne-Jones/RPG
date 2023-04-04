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


        public event Action onExperienceGained;
        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }

        public object CaptureState()
        {
            return experiencePoints;
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }
        public float GetFraction()
        {
            return experiencePoints / GetComponent<BaseStats>().GetStat(Stat.ExperienceToLevelUp);
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }
    }
}

