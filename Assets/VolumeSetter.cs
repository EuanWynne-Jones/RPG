using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace RPG.UI
{
    public class VolumeSetter : MonoBehaviour
    {
        [SerializeField] AudioMixer masterMixer;
        [SerializeField] public PlayerSettings playerSettings;
        public void setVolumes()
        {
            masterMixer.SetFloat("Master", playerSettings.masterVolume);
            masterMixer.SetFloat("Effects", playerSettings.effectsVolume);
            masterMixer.SetFloat("Voice", playerSettings.voiceVolume);
            masterMixer.SetFloat("Interface", playerSettings.interfaceVolume);
            masterMixer.SetFloat("Soundtrack", playerSettings.soundtrackVolume);
            masterMixer.SetFloat("Ambience", playerSettings.ambienceVolume);
        }
    }
}
