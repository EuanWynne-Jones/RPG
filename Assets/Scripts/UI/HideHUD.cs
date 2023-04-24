using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class HideHUD : MonoBehaviour
    {

        public GameObject[] hudElementsToHide; // an array of HUD elements to hide
        public GameObject dialogueHud; // a reference to the dialogue HUD object

        private bool isDialogueActive = false; // the current state of the dialogue HUD

        void Update()
        {
            // Check if the dialogue HUD is active
            bool newDialogueState = dialogueHud.activeSelf;

            // Only update if the state has changed
            if (newDialogueState != isDialogueActive)
            {
                isDialogueActive = newDialogueState;

                // Hide or show HUD elements based on the current state of the dialogue HUD
                foreach (GameObject hudElement in hudElementsToHide)
                {
                    if (hudElement.activeInHierarchy == false) return;
                    hudElement.SetActive(!isDialogueActive);
                }
            }
            else if (!isDialogueActive)
            {
                // Show HUD elements if the dialogue HUD is not active
                foreach (GameObject hudElement in hudElementsToHide)
                {
                    if (hudElement.activeInHierarchy == false) return;
                    hudElement.SetActive(true);
                }
            }
        }
    }

}



