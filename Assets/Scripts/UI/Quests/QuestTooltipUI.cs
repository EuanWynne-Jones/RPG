using RPG.Inventories;
using RPG.Quests;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.Quests
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI questName;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;

        [SerializeField] GameObject IncompleteObjectivePrefab;

        //[SerializeField] TextMeshProUGUI rewardText;
        [SerializeField] Transform rewardItemTranform;
        [SerializeField] GameObject rewardItem;

        [SerializeField] Transform rewardCurrencyTranform;
        [SerializeField] GameObject rewardCurrency;

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
            GetRewards(quest);
            GetRewardCurrency(quest);
            //rewardText.text = GetRewardText(quest);
        }

        private string GetRewardText(Quest quest)
        {
            string rewardText = "";
            foreach (var reward in quest.GetItemRewards())
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

        private void GetRewards(Quest quest)
        {
            foreach (var reward in quest.GetItemRewards())
            {
                GameObject instantiatedReward = Instantiate(rewardItem, rewardItemTranform);
                QuestReward QuestRewards = instantiatedReward.GetComponent<QuestReward>();
                QuestRewards.RewardIcon.sprite = reward.item.GetIcon();
                QuestRewards.RewardItemName.text = reward.item.GetDisplayName();
                QuestRewards.RewardDescription.text = reward.item.GetDescription();
            }
        }

        private void GetRewardCurrency(Quest quest)
        {

                foreach (Quest.CurrencyReward CR in quest.GetCurrencyRewards())
                {
                    GameObject instantiatedReward = Instantiate(rewardCurrency, rewardCurrencyTranform);
                    QuestCurrencyReward QCR = instantiatedReward.GetComponent<QuestCurrencyReward>();
                    Sprite currencyIcon = QCR.Icon.sprite;
                if (CR.currencyType == ECurrency.Gold)
                        {
                            currencyIcon = QCR.goldIcon;
                        }
                        if (CR.currencyType == ECurrency.Silver)
                        {
                            currencyIcon = QCR.silverIcon;
                        }
                        if (CR.currencyType == ECurrency.Copper)
                        {
                            currencyIcon = QCR.copperIcon;
                        }
                        QCR.GetComponentInChildren<Image>().sprite = currencyIcon;
                        QCR.amount.text = CR.amount.ToString();
                }


            
        }
    }
}
