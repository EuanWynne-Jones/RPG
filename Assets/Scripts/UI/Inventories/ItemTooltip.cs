using UnityEngine;
using TMPro;
using RPG.Inventories;
using UnityEngine.UI;
using System;
using RPG.Combat;
using RPG.SceneManagement;

namespace RPG.UI.Inventories
{
    /// <summary>
    /// Root of the tooltip prefab to expose properties to other classes.
    /// </summary>
    public class ItemTooltip : MonoBehaviour
    {
        // CONFIG DATA
        [SerializeField] TextMeshProUGUI titleText = null;
        [SerializeField] TextMeshProUGUI damageText = null;
        [SerializeField] TextMeshProUGUI qualityText = null;
        [SerializeField] TextMeshProUGUI descriptionText = null;
        [SerializeField] GameObject valuePrefab = null;
        [SerializeField] Transform valueParent = null;
        [SerializeField] GameObject itemIconPrefab;
        [SerializeField] Transform itemIconTransform;
        [SerializeField] Sprite goldIcon;
        [SerializeField] Sprite silverIcon;
        [SerializeField] Sprite copperIcon;

        public PlayerSettings playerSettings;
        // PUBLIC

        public void Setup(InventoryItem item)
        {
            SetupItemvalue(item);
            titleText.text = item.GetDisplayName();
            qualityText.text = item.GetItemQuality();
            SetupQualityColour(item);
            descriptionText.text = item.GetDescription();
            //SetupDamage(item);
            SetupItemIcon(item);
        }

        private void SetupItemIcon(InventoryItem item)
        {
            if (playerSettings.displayTooltipIcons)
            {
            GameObject instantiatedIcon = Instantiate(itemIconPrefab, itemIconTransform);
            instantiatedIcon.GetComponentInChildren<Image>().sprite = item.GetIcon();
            };
            
        }


        //Inventory Items need significant overhaul to allow for damage numbers to be passed down to base script...

        //private void SetupDamage(InventoryItem item)
        //{
        //    if(item)
        //    if (item.GetAllowedEquipLocation() != EquipLocation.Weapon) damageText.enabled = false;
        //    damageText.text = "Damage: " + item.GetMinDamage() + "- " + item.GetMaxDamage();
        //}


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
