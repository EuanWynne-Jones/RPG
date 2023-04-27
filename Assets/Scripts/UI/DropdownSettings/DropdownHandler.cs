using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DropdownHandler : MonoBehaviour
    {

        
        [SerializeField] private TMP_Dropdown dropdown;
        [SerializeField] private List<DropdownOptions> DropdownOptionsList = new List<DropdownOptions>();
        private string firstOption = null;
        private int lastSelectedOptionIndex = 0;
        public PlayerSettings playerSettings;

        [System.Serializable]
        public class DropdownOptions
        {
            [SerializeField] public string dropdownListItem;
            [SerializeField] public UnityEvent onDropdownSelect;

            public void InvokeEvent()
            {
                onDropdownSelect.Invoke();
            }
        }



        private void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();
            lastSelectedOptionIndex = GetLastValue();
            Setup();
            if (lastSelectedOptionIndex >= 0 && lastSelectedOptionIndex < DropdownOptionsList.Count)
            {
                dropdown.captionText.text = DropdownOptionsList[lastSelectedOptionIndex].dropdownListItem;
                dropdown.value = lastSelectedOptionIndex;
            }
            else if (firstOption != null)
            {
                dropdown.captionText.text = firstOption;
                dropdown.value = 0;
            }
            dropdown.RefreshShownValue();
        }

        private void OnDisable()
        {
            dropdown.ClearOptions();
            dropdown.onValueChanged.RemoveAllListeners();
            dropdown.captionText.text = "";
        }

        private void Setup()
        {
            dropdown.ClearOptions();
            foreach (DropdownOptions option in DropdownOptionsList)
            {
                if (firstOption == null)
                {
                    firstOption = option.dropdownListItem;
                }
                dropdown.options.Add(new TMP_Dropdown.OptionData() { text = option.dropdownListItem });
            }

            if (firstOption != null)
            {
                dropdown.captionText.text = firstOption;
                dropdown.value = 0;
            }
            dropdown.RefreshShownValue();
            dropdown.onValueChanged.AddListener(delegate
            {
                DropdownItemSelected(dropdown.value);
            });
        }

        private void DropdownItemSelected(int value)
        {
            //if (value != lastSelectedOptionIndex)
            //{
            //    DropdownOptionsList[value].InvokeEvent();
            //    lastSelectedOptionIndex = value;
            //}
            //dropdown.Hide();

            SetSettings(value);
            playerSettings.Save();

        }

        protected virtual void SetSettings(int value)
        {
            
        }

        protected virtual int GetLastValue()
        {
            return 0;
        }

    }
}

