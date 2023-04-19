using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class TextFocus : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;

        private void OnEnable()
        {
            inputField.ActivateInputField();
        }
    }
}