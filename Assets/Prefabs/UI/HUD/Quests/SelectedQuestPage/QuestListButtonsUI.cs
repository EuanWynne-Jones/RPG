using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestListButtonsUI : MonoBehaviour
    {
        [SerializeField] QuestPageUI questDetailsPrefab;
        [SerializeField] Transform selectedQuestPage;
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
                if (questStatus.IsFailed()) continue;
                QuestPageUI questInstance = Instantiate<QuestPageUI>(questDetailsPrefab, selectedQuestPage);
                questInstance.Setup(questStatus);
            }
        }
    }
}
