using UnityEngine;
using TMPro;
using RPG.Inventories;
using UnityEngine.UI;
using System;

namespace RPG.UI.Inventories
{
    /// <summary>
    /// Root of the tooltip prefab to expose properties to other classes.
    /// </summary>
    public class ItemTooltip : MonoBehaviour
    {
        // CONFIG DATA
        [SerializeField] TextMeshProUGUI titleText = null;
        [SerializeField] TextMeshProUGUI qualityText = null;
        [SerializeField] TextMeshProUGUI descriptionText = null;
        [SerializeField] GameObject valuePrefab = null;
        [SerializeField] Transform valueParent = null;
        [SerializeField] Sprite goldIcon;
        [SerializeField] Sprite silverIcon;
        [SerializeField] Sprite copperIcon;
        // PUBLIC

        public void Setup(InventoryItem item)
        {
            titleText.text = item.GetDisplayName();
            qualityText.text = item.GetItemQuality();
            SetupQualityColour(item);
            descriptionText.text = item.GetDescription();
            SetupItemvalue(item);
        }

        private void SetupQualityColour(InventoryItem item)
        {
            String returnquality = item.GetItemQuality();
            if (returnquality == "Common")
            {
                qualityText.color = Color.white;
            }
            if (returnquality == "Uncommon")
            {
                qualityText.color = Color.green;
            }
            if (returnquality == "Rare")
            {
                qualityText.color = Color.blue;
            }
            if (returnquality == "Epic")
            {
                qualityText.color = Color.magenta;
            }
            if (returnquality == "Ledgendary")
            {
                qualityText.color = new Color(1.0f, 0.64f, 0.0f);
            }
            if (returnquality == "Ledgendary")
            {
                qualityText.color = new Color(0.89f, 0.85f, 0.73f);
            }
        }
        public void SetupItemvalue(InventoryItem item)
        {
            
            if(item.GetGoldValue() > 1)
            {
                string value = item.GetGoldValue().ToString();
                GameObject instantiatedGoldValue = Instantiate(valuePrefab, valueParent);
                instantiatedGoldValue.GetComponent<Image>().sprite = goldIcon;
                instantiatedGoldValue.GetComponentInChildren<TextMeshProUGUI>().text = value;
            }
            if (item.GetSilveValue() > 1)
            {
                string value = item.GetSilveValue().ToString();
                GameObject instantiatedSilverValue = Instantiate(valuePrefab, valueParent);
                instantiatedSilverValue.GetComponent<Image>().sprite = silverIcon;
                instantiatedSilverValue.GetComponentInChildren<TextMeshProUGUI>().text = value;
            }
            if (item.GetCopperValue() > 1)
            {
                string value = item.GetCopperValue().ToString();
                GameObject instantiatedCopperValue = Instantiate(valuePrefab, valueParent);
                instantiatedCopperValue.GetComponent<Image>().sprite = copperIcon;
                instantiatedCopperValue.GetComponentInChildren<TextMeshProUGUI>().text = value;
            }

        }
    }
}
