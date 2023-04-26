using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.UI
{
    public class UIToggle : MonoBehaviour
    {
        public GameObject UIElement;

        public void ToggleElement()
        {
            if (UIElement.activeSelf)
            {
                UIElement.SetActive(false);
            }
            else
            {
                UIElement.SetActive(true);
            }
        }

    }

}
