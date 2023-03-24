using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestList : MonoBehaviour
    {
        List<QuestStatus> questStatuses = new List<QuestStatus>();
        public event Action onQuestListUpdated;
        public void AddQuest(Quest quest)
        {
            if (HasQuest(quest)) return;

            QuestStatus newStatus = new QuestStatus(quest);
            questStatuses.Add(newStatus);
            if(onQuestListUpdated != null)
            {
            onQuestListUpdated();
            }
        }

        public void CompleteObjective(Quest quest, string objective)
        {
            QuestStatus questStatus = GetQuestStatus(quest);
            questStatus.CompleteObjective(objective);
            if (onQuestListUpdated != null)
            {
                onQuestListUpdated();
            }
        }

        public bool HasQuest(Quest quest)
        {
            return GetQuestStatus(quest) != null;
        }
        public IEnumerable<QuestStatus> GetStatuses()
        {
            return questStatuses;
        }

        private QuestStatus GetQuestStatus(Quest quest)
        {
            foreach (QuestStatus questStatus in questStatuses)
            {
                if (questStatus.GetQuest() == quest)
                {
                    return questStatus;
                }
            }
            return null;
        }
    }
}
