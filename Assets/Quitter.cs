using RPG.Control;
using RPG.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.UI
{
    public class Quitter : MonoBehaviour
    {
        [SerializeField] int mainMenuScene = -1;
        Fader fader;

        private void Awake()
        {
            fader = FindObjectOfType<Fader>();
        }
        public void QuitGame()
        {
            fader.FadeOutImmediate();
            Application.Quit();
        }
    }

}