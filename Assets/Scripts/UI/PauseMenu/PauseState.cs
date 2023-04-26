using RPG.Control;
using RPG.Core;
using RPG.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class PauseState : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu = null;
        [SerializeField] GameObject UICanvas = null;
        PlayerController player;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }


        private void OnEnable()
        {
           Time.timeScale = 0f;
        }
        private void OnDisable()
        {
            Time.timeScale = 1f;
            player.GetComponent<ActionSchedueler>().CancelCurrentAction();
        }

        public void SaveAndQuit()
        {
            SavingWrapper savingWrapper =  FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();
            savingWrapper.LoadMainMenu();
        }



    }
}
