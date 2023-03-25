using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        
        [SerializeField] QuestItemUI questPrefab;
        QuestList questList;

        private void Start()
        {

            questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.onQuestListUpdated += Redraw;
            Redraw();
        }

        private void Redraw()
        {
            foreach (Transform item in transform)
            {
                Destroy(item.gameObject);
            }
            foreach (QuestStatus questStatus in questList.GetStatuses())
            {
                //removes completed quests from list
                if (questStatus.IsComplete()) continue;
                QuestItemUI uiInstance =  Instantiate<QuestItemUI>(questPrefab, transform);
                uiInstance.Setup(questStatus);
            }
        }

    }
}
