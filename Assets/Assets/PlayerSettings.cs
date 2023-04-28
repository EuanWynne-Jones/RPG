using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace RPG.UI
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Settings Config", menuName = "Settings", order = 0)]
    public class PlayerSettings : ScriptableObject  
    {
        [SerializeField] private string fileName = "/config.json";
        //GamePreset Options
        public enum GamePresets
        {
            Old_School,
            Stretched,
            Immersive
        }
        public GamePresets gamePresetOption;

        //HUD Display Options
        public enum HUDdisplayOptions
        {
            Center_Bottom,
            Outer_Bottom,
            Top_Left
        }
        public HUDdisplayOptions hudDisplayOption;


        //Enemy Heath Display Options
        public bool displayEnemyHeathOnCharacter;
        public bool displayEnemyHeathOnHUD;

        //Health Number options
        public bool displayHealthOnPlayer;
        public bool displayHealthOnNPCS;

        //Damage Number Options
        public bool displayDamageOnPlayer;
        public bool displayDamageOnNPCS;

        //Tutorial Options
        public bool displayTutorials;

        //Item ToolTip Icon Options
        public bool displayTooltipIcons;



        //Setting of saved data
        public void SetData(PlayerSettings playerSettings)
        {
            gamePresetOption = playerSettings.gamePresetOption;

            hudDisplayOption = playerSettings.hudDisplayOption;

            displayEnemyHeathOnCharacter = playerSettings.displayEnemyHeathOnCharacter;
            displayEnemyHeathOnHUD = playerSettings.displayEnemyHeathOnHUD;

            displayHealthOnPlayer = playerSettings.displayHealthOnPlayer;
            displayHealthOnNPCS = playerSettings.displayHealthOnNPCS;

            displayDamageOnPlayer = playerSettings.displayDamageOnPlayer;
            displayDamageOnNPCS = playerSettings.displayDamageOnNPCS;

            displayTutorials = playerSettings.displayTutorials;

            displayTooltipIcons = playerSettings.displayTooltipIcons;

        }

        private void SetData(SettingsSaveWrapper playerSettings)
        {
            gamePresetOption = playerSettings.gamePresetOption;
            hudDisplayOption = playerSettings.hudDisplayOption;
            displayEnemyHeathOnCharacter = playerSettings.displayEnemyHeathOnCharacter;
            displayEnemyHeathOnHUD = playerSettings.displayEnemyHeathOnHUD;

            displayDamageOnPlayer = playerSettings.displayDamageOnPlayer;
            displayDamageOnNPCS = playerSettings.displayDamageOnNPCS;
            displayHealthOnPlayer = playerSettings.displayHealthOnPlayer;
            displayHealthOnNPCS = playerSettings.displayHealthOnNPCS;
            displayTutorials = playerSettings.displayTutorials;
            displayTooltipIcons = playerSettings.displayTooltipIcons;

        }

        private class SettingsSaveWrapper
        {
            public SettingsSaveWrapper()
            {

            }
            public SettingsSaveWrapper(PlayerSettings playerSettings)
            {
                gamePresetOption = playerSettings.gamePresetOption;
                hudDisplayOption = playerSettings.hudDisplayOption;
                displayEnemyHeathOnCharacter = playerSettings.displayEnemyHeathOnCharacter;
                displayEnemyHeathOnHUD = playerSettings.displayEnemyHeathOnHUD;
                displayDamageOnPlayer = playerSettings.displayDamageOnPlayer;
                displayDamageOnNPCS = playerSettings.displayDamageOnNPCS;
                displayHealthOnPlayer = playerSettings.displayHealthOnPlayer;
                displayHealthOnNPCS = playerSettings.displayHealthOnNPCS;
                displayTutorials = playerSettings.displayTutorials;
                displayTooltipIcons = playerSettings.displayTooltipIcons;
            }
            
            public GamePresets gamePresetOption;

            public HUDdisplayOptions hudDisplayOption;


            //Enemy Heath Display Options
            public bool displayEnemyHeathOnCharacter;
            public bool displayEnemyHeathOnHUD;


            //Health Number options
            public bool displayHealthOnPlayer;
            public bool displayHealthOnNPCS;

            //Damage Number Options
            public bool displayDamageOnPlayer;
            public bool displayDamageOnNPCS;

            //Tutorial Options
            public bool displayTutorials;

            //Item ToolTip Icon Options
            public bool displayTooltipIcons;



        }

        [ContextMenu("save")]
        public void Save()
        {
            SettingsSaveWrapper wrapper = new SettingsSaveWrapper(this);

            string JSON = JsonUtility.ToJson(wrapper,true);
            File.WriteAllText(Application.dataPath + fileName, JSON);
            Debug.Log(Application.dataPath + fileName);
        }


        [ContextMenu("load")]
        public void Load()
        {
            if (File.Exists(Application.dataPath + fileName))
            {
                string JSON = File.ReadAllText(Application.dataPath + fileName);
                SetData(JsonUtility.FromJson<SettingsSaveWrapper>(JSON));
            }
            else Debug.Log("no save file");
        }

    }
}
