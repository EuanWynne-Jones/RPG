using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestCompletion : MonoBehaviour
    {
        [SerializeField] Quest quest;
        [SerializeField] string ObjectiveToComplete;


        public void CompleteObjective()
        {
            if(ObjectiveToComplete == "")
            {
                Debug.Log("Objective in QuestCompletion is Not Set!");
                return;
            }
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            if (questList.HasQuest(quest))
            {
            questList.CompleteObjective(quest, ObjectiveToComplete);
            }
            
            else
            {
                return;
            }
            
        }
    }
}
