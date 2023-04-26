using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class HUDToggle : MonoBehaviour
    {
        public PlayerSettings playerSettings;

        public GameObject centerBottomHUD;
        public GameObject outerBottomHUD;
        public GameObject topLeftHUD;

        private void Start()
        {
            SetUI();
        }
        public void SetUI()
        {
            
            centerBottomHUD.SetActive(playerSettings.hudDisplayOption == PlayerSettings.HUDdisplayOptions.Center_Bottom);
            outerBottomHUD.SetActive(playerSettings.hudDisplayOption == PlayerSettings.HUDdisplayOptions.Outer_Bottom);
            topLeftHUD.SetActive(playerSettings.hudDisplayOption == PlayerSettings.HUDdisplayOptions.Top_Left);

            /*
            switch (settingsScriptableObject.hUDdisplayOption)
            {

                case SettingsScriptableObject.HUDdisplayOptions.Center_Bottom:
                    
                    //toggle CenterHUD
                    break;
                case SettingsScriptableObject.HUDdisplayOptions.Outer_Bottom:
                    //toggle CenterHUD
                    break;
                case SettingsScriptableObject.HUDdisplayOptions.Top_Left:
                    //toggle CenterHUD
                    break;
                default:
                    Debug.Log("Default");
                    //toggle default HUD
                    break;

            }
            */

        }
    }
}
