using RPG.Core;
using RPG.Inventories;
using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Quests
{
    public class QuestList : MonoBehaviour, ISaveable, IPredicateEvaluator
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
            if (questStatus.IsComplete())
            {
                GiveReward(quest);
                Debug.Log("Rewards given");
            }
            if (onQuestListUpdated != null)
            {
                onQuestListUpdated();
                Debug.Log("UI updated");
            }
            Debug.Log("Quest Completed");
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


        private void GiveReward(Quest quest)
        {
            foreach (Quest.Reward reward in quest.GetRewards())
            {
                if (!reward.item.IsStackable())
                {
                    int given = 0;

                    for (int i = 0; i < reward.number; i++)
                    {
                        bool isGiven = GetComponent<Inventory>().AddToFirstEmptySlot(reward.item, 1);
                        if (!isGiven) break;
                        given++;
                    }

                    if (given == reward.number) continue;

                    for (int i = given; i < reward.number; i++)
                    {
                        GetComponent<ItemDropper>().DropItem(reward.item, 1);
                    }
                }
                else
                {
                    bool isGiven = GetComponent<Inventory>().AddToFirstEmptySlot(reward.item, reward.number);
                    if (!isGiven)
                    {
                        for (int i = 0; i < reward.number; i++)
                        {
                            GetComponent<ItemDropper>().DropItem(reward.item, reward.number);
                        }
                    }

                }
            }



        }

        public bool? Evaluate(string predicate, string[] parameters)
        {
            switch (predicate)
            {
                case "HasQuest":
                    return HasQuest(Quest.GetByName(parameters[0]));
                case "CompletedQuest":
                    return GetQuestStatus(Quest.GetByName(parameters[0])).IsComplete();
            }

            return null;
        }

        public object CaptureState()
        {
            List<object> state = new List<object>();
            foreach (QuestStatus status in questStatuses)
            {
                state.Add(status.CaptureState());
            }
            return state;
        }

        public void RestoreState(object state)
        {
            List<object> stateList = state as List<object>;
            if (stateList == null) return;

            questStatuses.Clear();
            foreach (object objectState in stateList)
            {
                questStatuses.Add(new QuestStatus(objectState));
               
            }

        }


    }
}
