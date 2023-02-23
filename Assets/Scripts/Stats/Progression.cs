using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookuptable = null;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();
            float[] levels = lookuptable[characterClass][stat];
            if (levels.Length < level)
            {
                return 0;
            }
            return levels[level -1];
        }

        private  void BuildLookup()
        {
            if (lookuptable != null) return;
            lookuptable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach(ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.Levels;
                }

                lookuptable[progressionClass.characterClass] = statLookupTable;
            }

        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();
            float[] levels = lookuptable[characterClass][stat];
            return levels.Length;
        }

        [System.Serializable]
        class ProgressionCharacterClass 
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
             


        }
        [System.Serializable]
        public class ProgressionStat
        {
            public Stat stat;
            public float[] Levels;
        }

        



    }
}
