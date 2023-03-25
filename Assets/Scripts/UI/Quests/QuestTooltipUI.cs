using RPG.Quests;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.UI.Quests
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI questName;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;
        [SerializeField] GameObject IncompleteObjectivePrefab;
        [SerializeField] TextMeshProUGUI rewardText;

        public void Setup(QuestStatus questStatus)
        {
            Quest quest = questStatus.GetQuest();
            questName.text = questStatus.GetQuest().GetTitle();

            foreach (Transform item in objectiveContainer)
            {
                Destroy(item.gameObject);
            }
            foreach (var objective in questStatus.GetQuest().GetObjectives())
            {
                GameObject prefab = IncompleteObjectivePrefab;
                if (questStatus.IsObjectiveComplete(objective.reference))
                {
                    prefab = objectivePrefab;
                }
                GameObject objectiveInstance = Instantiate(prefab, objectiveContainer);
                TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                objectiveText.text = objective.description;

            }
            rewardText.text = GetRewardText(quest);
        }

        private string GetRewardText(Quest quest)
        {
            string rewardText = "";
            foreach (var reward in quest.GetRewards())
            {
                if (rewardText != "")
                {
                    rewardText += ", ";
                }
                if (reward.amount > 1)
                {
                    rewardText += reward.amount + " ";
                }
                rewardText += reward.item.GetDisplayName();
            }
            if (rewardText == "")
            {
                rewardText = "No reward";
            }
            rewardText += ".";
            return rewardText;
        }
    }
}
