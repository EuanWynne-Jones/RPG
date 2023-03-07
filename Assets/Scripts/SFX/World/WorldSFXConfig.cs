using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    [CreateAssetMenu(fileName = "SceneSoundScape", menuName = "WorldSFX/ Make New SceneSoundScape", order = 0)]
    public class WorldSFXConfig : ScriptableObject
    {
        [Header("SoundTrack")]
        [SerializeField] public string sountrackSourceName = "Soundtrack";
        [SerializeField] public AudioSource soundtrackSource;
        [SerializeField] private List<AudioClip> soundtrackClips;

        [Header("Ambience")]
        [SerializeField] public string ambienceSourceName = "Ambience";
        [SerializeField] public AudioSource ambienceSource;
        [SerializeField] private List<AudioClip> ambienceClips;

        [Header("Weather")]
        [SerializeField] public string weatherSourceName = "Weather";
        [SerializeField] public AudioSource weatherSource;
        [SerializeField] private List<AudioClip> weatherClips;


        private List<AudioClip> soundtrackClipsList
        {
            get{return soundtrackClips;}
        }

        private List<AudioClip> ambienceClipsList
        {
            get{return ambienceClips;}
        }

        private List<AudioClip> weatherClipsList
        {
            get{return weatherClips;}
        }

        private AudioClip soundtrackClip
        {
            get
            {
                if (soundtrackClipsList.Count > 0)
                {
                    return soundtrackClipsList[Random.Range(0, soundtrackClipsList.Count)];
                }
                return null;
            }
        }

        private AudioClip ambienceClip
        {
            get
            {
                if (ambienceClipsList.Count > 0)
                {
                    return ambienceClipsList[Random.Range(0, ambienceClipsList.Count)];
                }
                return null;
            }
        }

        private AudioClip weatherClip
        {
            get
            {
                if (weatherClipsList.Count > 0)
                {
                    return weatherClipsList[Random.Range(0, weatherClipsList.Count)];
                }
                return null;
            }
        }

        private void PlaySoundtrack(AudioClip audioClip)
        {
            if (soundtrackSource != null && audioClip != null)
            {
                //if (soundtrackSource.isPlaying) soundtrackSource.Stop();
                soundtrackSource.clip = audioClip;
                soundtrackSource.Play();
            }
        }

        private void Playambience(AudioClip audioClip)
        {
            if (ambienceSource != null && audioClip != null)
            {
                if (ambienceSource.isPlaying) ambienceSource.Stop();
                ambienceSource.clip = audioClip;
                ambienceSource.Play();
            }
        }

        private void Playweather(AudioClip audioClip)
        {
            if (weatherSource != null && audioClip != null)
            {
                if (weatherSource.isPlaying) weatherSource.Stop();
                weatherSource.clip = audioClip;
                weatherSource.Play();
            }
        }

        public void SoundtrackTrigger()
        {
;
            if (soundtrackClips.Count > 0)
            {
                PlaySoundtrack(soundtrackClip);
            }


        }

        public void AmbienceTrigger()
        {
            if (ambienceSource.isPlaying) return;
            if (ambienceClips.Count > 0)
            {
                Playambience(ambienceClip);
            }
        }

        public void WeatherTrigger()
        {
            if (weatherSource.isPlaying) return;
            if (weatherClips.Count > 0)
            {
                Playweather(weatherClip);
            }
        }



    }
}

