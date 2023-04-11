using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Quests
{
    public class QuestCurrencyReward : MonoBehaviour
    {
        [SerializeField] public Sprite goldIcon;
        [SerializeField] public Sprite silverIcon;
        [SerializeField] public Sprite copperIcon;

        [SerializeField] public Image Icon;
        [SerializeField] public TextMeshProUGUI amount;

    }
}
