using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestPageSpawner : MonoBehaviour
    {
        [SerializeField] Transform selectedQuestPage;
        [SerializeField] GameObject questDetailsPrefab;
        GameObject currentlyDisplyedQuest = null;

        
        public void CreateQuestPage()
        {
            QuestStatus questStatus = GetComponentInChildren<QuestItemUI>().GetQuestStatus();
            if(selectedQuestPage.gameObject.activeInHierarchy == false)
            {
            selectedQuestPage.gameObject.SetActive(true);
            }
            GameObject instantiatedQuestDetails = Instantiate(questDetailsPrefab, selectedQuestPage);
            currentlyDisplyedQuest = instantiatedQuestDetails;
            currentlyDisplyedQuest.GetComponent<QuestPageUI>().Setup(questStatus);
        }
    }
}
