using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Settings Config", menuName = "Settings", order = 0)]
    public class PlayerSettings : ScriptableObject  
    {

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
            displayDamageOnPlayer = playerSettings.displayDamageOnPlayer;
            displayDamageOnNPCS = playerSettings.displayDamageOnNPCS;
            displayTutorials = playerSettings.displayTutorials;
            displayTooltipIcons = playerSettings.displayTooltipIcons;

        }

    }
}
