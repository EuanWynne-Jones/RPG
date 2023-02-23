using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,10)]
        [SerializeField] int startingLevel = 1;


        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        private void Update()
        {
            if(gameObject.tag == "Player")
            {
                print(GetLevel());
            }
            
        }

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat,characterClass, startingLevel);
        }

        public int GetLevel()
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
