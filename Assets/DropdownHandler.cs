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

        private void OnEnable()
        {
            Setup();
        }

        private void Setup()
        {
            dropdown.ClearOptions();

            foreach (DropdownOptions option in DropdownOptionsList)
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData() { text = option.dropdownListItem });
            }

            dropdown.onValueChanged.AddListener(delegate
            {
                DropdownItemSelected(dropdown.value);
            });
        }

        private void DropdownItemSelected(int value)
        {
            DropdownOptionsList[value].InvokeEvent();
        }

    }
}
