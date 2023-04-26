using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class ActionSlotKey : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI slotNumberText;
        [SerializeField] string slotKey;

        private void Start()
        {
            slotNumberText.text = slotKey;
        }
    }

}