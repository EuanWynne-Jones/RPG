using RPG.Quests;
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
        public void Setup(QuestStatus questStatus)
        {
            questName.text = questStatus.GetQuest().GetTitle();
            foreach (GameObject objectivePrefab in objectiveContainer)
            {
                Destroy(objectivePrefab);
            }
            foreach (string objective in questStatus.GetQuest().GetObjectives())
            {
                GameObject prefab = IncompleteObjectivePrefab;
                if (questStatus.IsObjectiveComplete(objective))
                {
                    prefab = objectivePrefab;
                }
                GameObject objectiveInstance =  Instantiate(prefab, objectiveContainer);
                TextMeshProUGUI objectiveText = objectiveInstance.GetComponentInChildren<TextMeshProUGUI>();
                objectiveText.text = objective;

            }
        }
    }
}
