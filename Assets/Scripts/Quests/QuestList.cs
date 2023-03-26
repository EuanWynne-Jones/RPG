using RPG.Inventories;
using RPG.Saving;
using RPG.Utils;
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

        private void Update()
        {
            CompleteObjectivesByPredicates();
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

                    for (int i = 0; i < reward.amount; i++)
                    {
                        bool isGiven = GetComponent<Inventory>().AddToFirstEmptySlot(reward.item, 1);
                        if (!isGiven) break;
                        given++;
                    }

                    if (given == reward.amount) continue;

                    for (int i = given; i < reward.amount; i++)
                    {
                        GetComponent<ItemDropper>().DropItem(reward.item, 1);
                    }
                }
                else
                {
                    bool isGiven = GetComponent<Inventory>().AddToFirstEmptySlot(reward.item, reward.amount);
                    if (!isGiven)
                    {
                        for (int i = 0; i < reward.amount; i++)
                        {
                            GetComponent<ItemDropper>().DropItem(reward.item, reward.amount);
                        }
                    }

                }
            }



        }

        private void CompleteObjectivesByPredicates()
        {
            foreach (QuestStatus status in questStatuses)
            {
                if (status.IsComplete()) continue;
                Quest quest = status.GetQuest();
                foreach (var objective in status.GetQuest().GetObjectives())
                {
                    if (status.IsObjectiveComplete(objective.reference)) continue;
                    if (!objective.usesCondition) continue;
                    if (objective.completionCondition.Check(GetComponents<IPredicateEvaluator>()))
                    {
                        CompleteObjective(quest, objective.reference);
                    }
                }
            }
        }

        public bool? Evaluate(EPredicate predicate, string[] parameters)
        {
            switch (predicate)
            {
                case EPredicate.HasQuest:
                    return HasQuest(Quest.GetByName(parameters[0]));
                case EPredicate.CompletedQuest:
                    QuestStatus status = GetQuestStatus(Quest.GetByName(parameters[0]));
                    if (status == null) return false;
                    return status.IsComplete();
                return GetQuestStatus(Quest.GetByName(parameters[0])).IsComplete();
                case EPredicate.CompletedObjective:
                    QuestStatus teststatus = GetQuestStatus(Quest.GetByName(parameters[0]));
                    if (teststatus == null) return false;
                    return teststatus.IsObjectiveComplete(parameters[1]);
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
