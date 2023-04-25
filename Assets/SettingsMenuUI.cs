using RPG.UI.Inventories;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace RPG.UI
{
    public class SettingsMenuUI : MonoBehaviour
    {
        [Header("DefaultTab")]
        [SerializeField] GameObject defaultTab;

        [Header("Video Settings")]
        [SerializeField] public TMP_Dropdown displayDropdown;
        [SerializeField] public TMP_Dropdown resolutionDropdown;

        [Header("Sound Settings")]
        [SerializeField] public AudioMixer masterMixer;
        [SerializeField] public Slider masterVolumeSlider;
        [HideInInspector]
        [SerializeField] float masterValue;

        [SerializeField] public Slider SoundtrackVolumeSlider;
        [HideInInspector]
        [SerializeField] float soundtrackValue;

        [SerializeField] public Slider AmbienceVolumeSlider;
        [HideInInspector]
        [SerializeField] float ambienceValue;

        [SerializeField] public Slider GameplayVolumeSlider;
        [HideInInspector]
        [SerializeField] float gameplayValue;

        [SerializeField] public Slider DialogueVolumeSlider;
        [HideInInspector]
        [SerializeField] float dialogueValue;


        [Header("Gameplay Settings")]
        [SerializeField] TMP_Dropdown HUDOrientationDropdown;
        [SerializeField] TMP_Dropdown questIndicationDropdown;
        [SerializeField] TMP_Dropdown onScreenObjectivesDropdown;
        [SerializeField] TMP_Dropdown tutorialNotificationsDropdown;



        
        private void OnEnable()
        {
            defaultTab.SetActive(true);
            masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
            SoundtrackVolumeSlider.value = PlayerPrefs.GetFloat("SoundtrackVolume");
            AmbienceVolumeSlider.value = PlayerPrefs.GetFloat("AmbienceVolume");
            GameplayVolumeSlider.value = PlayerPrefs.GetFloat("GameplayVolume");
            DialogueVolumeSlider.value = PlayerPrefs.GetFloat("DialogueVolume");
        }


        public void SetMasterVolume()
        {
            masterValue = masterVolumeSlider.value;
            masterMixer.SetFloat("Master", masterValue);
            PlayerPrefs.SetFloat("MasterVolume", masterValue);
        }

        public void SetSoundtrackVolume()
        {
            soundtrackValue = SoundtrackVolumeSlider.value;
            masterMixer.SetFloat("Soundtrack", soundtrackValue);
            PlayerPrefs.SetFloat("SoundtrackVolume", soundtrackValue);
        }
        public void SetAmbienceVolume()
        {
            ambienceValue = AmbienceVolumeSlider.value;
            masterMixer.SetFloat("Ambience", ambienceValue);
            PlayerPrefs.SetFloat("AmbienceVolume", ambienceValue);
        }
        public void SetGameplayVolume()
        {
            gameplayValue = GameplayVolumeSlider.value;
            masterMixer.SetFloat("Gameplay", gameplayValue);
            PlayerPrefs.SetFloat("GameplayVolume", gameplayValue);
        }
        public void SetDialogueVolume()
        {
            dialogueValue = DialogueVolumeSlider.value;
            masterMixer.SetFloat("Dialogue", dialogueValue);
            PlayerPrefs.SetFloat("DialogueVolume", dialogueValue);
        }




        public void SaveSettings()
        {
            PlayerPrefs.Save();
        }

    }
}
