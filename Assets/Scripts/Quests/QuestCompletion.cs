using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestCompletion : MonoBehaviour
    {
        [SerializeField] Quest quest;
        [SerializeField] string objectiveToComplete;
        QuestStatus questStatus;

        public void CompleteObjective()
        {
            if(objectiveToComplete == string.Empty)
            {
                Debug.Log("Objective in QuestCompletion is Not Set!");

                return;
            }
            
            QuestList questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            
            if (questList.HasQuest(quest) && !quest.GetIsFailed(quest))
            {
                questList.CompleteObjective(quest, objectiveToComplete);
            }
            else
            {
                return;
            }
            
        }
    }
}
