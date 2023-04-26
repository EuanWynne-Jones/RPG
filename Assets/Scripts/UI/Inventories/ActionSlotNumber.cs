using RPG.UI.Inventories;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class ActionSlotNumber : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI slotNumberText;
        [SerializeField] ActionSlotUI slotNumberUI;

        private void Start()
        {
            int realIndex = slotNumberUI.index + 1;
            slotNumberText.text = realIndex.ToString();
        }
    }
}
