using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class TutorialTrigger : MonoBehaviour
    {
        PopupHandler popupHandler;
        [SerializeField] string tutorialText;

        private void Awake()
        {
            popupHandler = FindObjectOfType<PopupHandler>();
        }

        private void OnTriggerEnter(Collider other)
        {
            popupHandler.spawnTutorialPopup(tutorialText);
        }

        private void OnTriggerExit(Collider other)
        {
            gameObject.SetActive(false);
        }
    }
}
