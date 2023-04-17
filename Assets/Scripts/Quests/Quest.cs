using RPG.Inventories;
using RPG.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quests",order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] List<Objective> objectives = new List<Objective>();
        [SerializeField] List<Reward> rewards = new List<Reward>();
        [SerializeField] List<CurrencyReward> currencyRewards = new List<CurrencyReward>();
        [SerializeField] float experienceReward = 0;

        [System.Serializable]
        public class Reward
        {
            public InventoryItem item;
            [Min(1)]
            public int amount;
            
        }

        [System.Serializable]
        public class CurrencyReward
        {
            public ECurrency currencyType;
            [Range(0,99)]
            public int amount;


        }

        [System.Serializable]
        public class Objective
        {
            public string description;
            public string reference;
            public bool usesCondition = false;
            public Condition completionCondition;

        }
        public string GetTitle()
        {
            return name;
        }



        public int GetObjectiveCount()
        {
            return objectives.Count;
        }
        

        public float GetExpereienceReward()
        {
            return experienceReward;
        }

        public IEnumerable<Objective> GetObjectives()
        {
            return objectives;
        }
        public IEnumerable<CurrencyReward> GetCurrencyRewards()
        {

            return currencyRewards;
        }
        public IEnumerable<Reward> GetItemRewards()
        {
            
            return rewards;
        }



        public bool HasObjective(string objectiveRef)
        {
            foreach (var objective in objectives)
            {
                if(objective.reference == objectiveRef)
                {
                    return true;
                }
            }
            return false;
            {

            }
        }

        public static Quest GetByName(string questName)
        {
            foreach(Quest quest in Resources.LoadAll<Quest>(""))
            {
                if(quest.name == questName)
                { 
                    return quest;
                }
            }
            return null;
            
        }
    }
}
