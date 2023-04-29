using RPG.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class ProjectileSFX : MonoBehaviour
    {
        [Header("ProjectileLaunch")]
        private string projectileLaunchName = "Attack";
        [SerializeField] public AudioSource projectileLaunchSource;
        [SerializeField] public List<AudioClip> projectileLaunchSounds;

        [Header("ProjectileImpact")]
        private string projectileImpactName = "Impact";
        [SerializeField] public AudioSource projectileImpactSource;
        [SerializeField] public List<AudioClip> projectileImpactSounds;

        [Header("AudioSourceOverrite")]
        [SerializeField] WeaponAudioOverrite audioOverrite = null;

        ProjectileSFXHandler projectileSFXHandler = null;
        AudioMixerHandler mixerHandler;

        private void Awake()
        {
            if(mixerHandler == null)
            {
                mixerHandler = FindObjectOfType<AudioMixerHandler>();
            }
            projectileSFXHandler = GetComponent<ProjectileSFXHandler>();
            projectileSFXHandler.projectileSFX = gameObject.GetComponent<ProjectileSFX>();
            SetupAudioSources();
        }

        private AudioClip projectileLaunchSoundClip
        {
            get
            {
                return projectileLaunchSounds[Random.Range(0, projectileLaunchSounds.Count)];

            }
        }
        private AudioClip projectileImpactSoundClip
        {
            get
            {
                return projectileImpactSounds[Random.Range(0, projectileImpactSounds.Count)];
            }
        }

        private void PlayLaunchSound(AudioClip audioClip)
        {
            if (projectileLaunchSource != null && audioClip != null)
            {
                if (projectileLaunchSource.isPlaying) projectileLaunchSource.Stop();
                projectileLaunchSource.clip = audioClip;
                projectileLaunchSource.Play();
            }
        }

        private void PlayImpactSound(AudioClip audioClip)
        {
            if (projectileImpactSource != null && audioClip != null)
            {
                if (projectileImpactSource.isPlaying) projectileImpactSource.Stop();
                projectileImpactSource.clip = audioClip;
                projectileImpactSource.Play();
            }
        }

        public void PlayLaunching()
        {
            if (projectileLaunchSounds.Count > 0)
            {
                PlayLaunchSound(projectileLaunchSoundClip);
            }
            else Debug.Log("No audioclips found");
        }
        public void PlayImpacting()
        {

            if (projectileImpactSounds.Count > 0)
            {
                PlayImpactSound(projectileImpactSoundClip);
            }
            else Debug.Log("No audioclips found");
        }

        public void SetupAudioSources()
        {
            Transform[] transforms = GetComponentsInChildren<Transform>();

            if (projectileLaunchName == "") Debug.LogError(name + " missing ProjectileLaunchName");
            if (projectileImpactName == "") Debug.LogError(name + " missing ProjectileImpactName");
            if (transforms.Length > 0)
            {
                foreach (Transform t in transforms)
                {
                    if (projectileLaunchSource == null && t.name == projectileLaunchName)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            projectileLaunchSource = audioSource;
                        }
                        else
                        {
                            projectileLaunchSource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }

                    if (projectileImpactSource == null && t.name == projectileImpactName)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            projectileImpactSource = audioSource;
                        }
                        else
                        {
                            projectileImpactSource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }
                }
                if (projectileLaunchSource == null) Debug.LogError(name + " ProjectileLaunchSource not set");
                if (projectileImpactSource == null) Debug.LogError(name + " ProjectileImpactSource not set");

                if (projectileLaunchSource != null)
                {
                    projectileLaunchSource.playOnAwake = audioOverrite.playOnAwake;
                    projectileLaunchSource.spatialBlend = audioOverrite.spacialBlend;
                    projectileLaunchSource.maxDistance = audioOverrite.maxDistance;
                    projectileLaunchSource.volume = audioOverrite.weaponVolume;
                    projectileLaunchSource.outputAudioMixerGroup = mixerHandler.effects;

                }

                if (projectileImpactSource != null)
                {
                    projectileImpactSource.playOnAwake = audioOverrite.playOnAwake;
                    projectileImpactSource.spatialBlend = audioOverrite.spacialBlend;
                    projectileImpactSource.maxDistance = audioOverrite.maxDistance;
                    projectileImpactSource.volume = audioOverrite.weaponVolume;
                    projectileImpactSource.outputAudioMixerGroup = mixerHandler.effects;
                }
            }

        }
    }
}
