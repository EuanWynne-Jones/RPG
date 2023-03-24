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
            foreach (QuestItemUI quest in transform)
            {
                Destroy(quest);
            } 
            foreach (QuestStatus questStatus in questList.GetStatuses())
            {
                QuestItemUI uiInstance = Instantiate<QuestItemUI>(questPrefab, transform);
                uiInstance.Setup(questStatus);
            }
        }

    }
}
