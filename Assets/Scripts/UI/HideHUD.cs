using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class HideHUD : MonoBehaviour
    {

        public GameObject currentHUDToHide = null;
        public GameObject dialogueHud; // a reference to the dialogue HUD object

        private bool isDialogueActive = false; // the current state of the dialogue HUD

        private void Awake()
        {
            GetHUD();
        }

        public void GetHUD()
        {
            currentHUDToHide = FindObjectOfType<HUDOptionUI>().gameObject;
        }

        void Update()
        {
            // Check if the dialogue HUD is active
            bool newDialogueState = dialogueHud.activeSelf;

            // Only update if the state has changed
            if (newDialogueState != isDialogueActive)
            {
                isDialogueActive = newDialogueState;

                // Hide or show HUD elements based on the current state of the dialogue HUD
                currentHUDToHide.SetActive(!isDialogueActive);
            }
            else if (!isDialogueActive)
            {
                currentHUDToHide.SetActive(true);
            }
        }
    }

}



