using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class LoadSettings : MonoBehaviour
    {

       [SerializeField] PlayerSettings playerSettings;

        private void Start()
        {
            playerSettings.Load();
        }
    }
}
