using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Quests
{
    public class QuestReward : MonoBehaviour
    {
        [SerializeField] public Image RewardIcon = null;
        [SerializeField] public TextMeshProUGUI RewardItemName = null;
        [SerializeField] public TextMeshProUGUI RewardDescription = null;

    }

}
