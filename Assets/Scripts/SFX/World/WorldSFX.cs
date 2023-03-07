using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class WorldSFX : MonoBehaviour
    {

        [SerializeField] public SoundscapeSetter soundscapeSetter;
        public WorldSFXConfig worldSFXConfig;

        public void GetSoundScape()
        {

            soundscapeSetter = FindObjectOfType<SoundscapeSetter>();
            if (soundscapeSetter == null) print("Cant access soundsetter");
            worldSFXConfig = soundscapeSetter.currentWorldSFXConfig;
        }

        private void LateUpdate()
        {
            if (worldSFXConfig != null)
            {

                worldSFXConfig.AmbienceTrigger();
                worldSFXConfig.WeatherTrigger();
            }
        }
        public void SetupAudioSources()
        {


            Transform[] transforms = GetComponentsInChildren<Transform>();

            if (worldSFXConfig.sountrackSourceName == "") Debug.LogError(name + " missing sountrackSourceName");
            if (worldSFXConfig.ambienceSourceName == "") Debug.LogError(name + " missing ambienceSourceName");
            if (worldSFXConfig.weatherSourceName == "") Debug.LogError(name + " missing weatherSourceName");


            if (transforms.Length > 0)
            {
                foreach (Transform t in transforms)
                {
                    if (worldSFXConfig.soundtrackSource == null && t.name == worldSFXConfig.sountrackSourceName)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            worldSFXConfig.soundtrackSource = audioSource;
                        }
                        else
                        {
                            worldSFXConfig.soundtrackSource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }

                    if (worldSFXConfig.ambienceSource == null && t.name == worldSFXConfig.ambienceSourceName)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            worldSFXConfig.ambienceSource = audioSource;
                        }
                        else
                        {
                            worldSFXConfig.ambienceSource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }

                    if (worldSFXConfig.weatherSource == null && t.name == worldSFXConfig.weatherSourceName)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            worldSFXConfig.weatherSource = audioSource;
                        }
                        else
                        {
                            worldSFXConfig.weatherSource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }

                }
            }

            if (worldSFXConfig.soundtrackSource == null) Debug.LogError(name + " soundtrackSource not set.");
            if (worldSFXConfig.ambienceSource == null) Debug.LogError(name + " ambienceSource not set.");
            if (worldSFXConfig.weatherSource == null) Debug.LogError(name + " weatherSource not set.");



        }

    }
}

