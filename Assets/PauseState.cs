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



        private void Start()
        {
            pauseMenu.SetActive(false);
        }

        private void Update()
        {
            if(pauseMenu.activeInHierarchy == true)
            {
                Time.timeScale = 0f;
                UICanvas.SetActive(false);
            }
            else Time.timeScale = 1f;
            UICanvas.SetActive(true);

        }


       
    }
}
