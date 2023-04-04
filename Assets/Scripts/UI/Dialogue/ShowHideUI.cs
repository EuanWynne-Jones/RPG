using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace RPG.UI
{
    public class ShowHideUI : MonoBehaviour
    {
        // A struct that holds a HUD element and its toggle key
        [System.Serializable]
        public struct HUDObject
        {
            public GameObject element;
            public KeyCode toggleKey;
            public List<GameObject> disableOthers;
        }

        // An array of HUD objects to toggle
        public HUDObject[] hudObjects;

        void Start()
        {
            // Disable all HUD elements by default
            for (int i = 0; i < hudObjects.Length; i++)
            {
                hudObjects[i].element.SetActive(false);
            }
        }

        void Update()
        {
            // Check if any of the toggle keys have been pressed
            for (int i = 0; i < hudObjects.Length; i++)
            {
                if (Input.GetKeyDown(hudObjects[i].toggleKey))
                {
                    if (hudObjects[i].element.activeSelf)
                    {
                        DisableOtherHUDObjects(hudObjects[i].disableOthers);
                    }
                    else
                    {
                        EnableHUDObject(hudObjects[i].element);
                    }
                }
            }
        }

        // Enables a HUD object and disables all others
        void EnableHUDObject(GameObject objToEnable)
        {
            // Enable the requested HUD object
            objToEnable.SetActive(true);

            // Disable all other HUD objects
            for (int i = 0; i < hudObjects.Length; i++)
            {
                if (hudObjects[i].element != objToEnable && hudObjects[i].disableOthers.Contains(objToEnable))
                {
                    hudObjects[i].element.SetActive(false);
                }
            }
        }

        // Disables all HUD objects except the ones passed in
        void DisableOtherHUDObjects(List<GameObject> objsToExclude)
        {
            for (int i = 0; i < hudObjects.Length; i++)
            {
                if (hudObjects[i].element != null && !objsToExclude.Contains(hudObjects[i].element))
                {
                    hudObjects[i].element.SetActive(false);
                }
            }
        }
    }
}
