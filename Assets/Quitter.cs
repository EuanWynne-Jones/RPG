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

        public void QuitGame()
        {
            Application.Quit();
        }
    }

}