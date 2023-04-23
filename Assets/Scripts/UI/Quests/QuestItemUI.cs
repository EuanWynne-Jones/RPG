using RPG.Quests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Quests
{
    public class QuestItemUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI progress;
        [SerializeField] Toggle isActiveQuest;
        QuestStatus questStatus;
        public void Setup(QuestStatus questStatus)
        {
            this.questStatus = questStatus;
            title.text = questStatus.GetQuest().GetTitle();
            progress.text = questStatus.GetCompletedCount() + "/" + questStatus.GetQuest().GetObjectiveCount();
        }

        public QuestStatus GetQuestStatus()
        {
            return questStatus;
        }


    }
}
