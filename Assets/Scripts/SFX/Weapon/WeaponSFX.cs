using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;
using RPG.UI;

namespace RPG.Combat
{
    public class WeaponSFX : MonoBehaviour
    {


        [Header("WeaponAttack")]
        private string weaponAttackName = "Attack";
        [SerializeField] public AudioSource weaponAttackSource;
        [SerializeField] public List<AudioClip> attackSounds;

        [Header("WeaponImpact")]
        private string weaponImpactname = "Impact";
        [SerializeField] public AudioSource weaponImpactSource;
        [SerializeField] public List<AudioClip> impactSounds;

        [Header("AudioSourceOverrite")]
        [SerializeField] WeaponAudioOverrite audioOverrite = null;

        SFXHandler SFXHandler = null;
        AudioMixerHandler mixerHandler;

        private void Awake()
        {
            if(mixerHandler == null)
            {
                mixerHandler = FindObjectOfType<AudioMixerHandler>();
            }
        }
        private void Start()
        {
            SFXHandler = GetComponentInParent<SFXHandler>();
            SFXHandler.weaponSFX = gameObject.GetComponent<WeaponSFX>();
            SetupAudioSources();
        }

        private AudioClip attackingSoundClip
        {
            get
            {
                return attackSounds[Random.Range(0, attackSounds.Count)];

            }
        }
        private AudioClip impactingSoundClip
        {
            get
            {
                return impactSounds[Random.Range(0, impactSounds.Count)];
            }
        }

        private void PlayAttackSound(AudioClip audioClip)
        {
            if (weaponAttackSource != null && audioClip != null)
            {
                if (weaponAttackSource.isPlaying) weaponAttackSource.Stop();
                weaponAttackSource.clip = audioClip;
                weaponAttackSource.Play();
            }
        }

        private void PlayImpactSound(AudioClip audioClip)
        {
            if (weaponImpactSource != null && audioClip != null)
            {
                if (weaponImpactSource.isPlaying) weaponImpactSource.Stop();
                weaponImpactSource.clip = audioClip;
                weaponImpactSource.Play();
            }
        }

        public void PlayImpacting()
        {

            if (impactSounds.Count > 0)
            {
                PlayImpactSound(impactingSoundClip);
            }
            else return;
        }
        public void PlayAttacking()
        {

            if (attackSounds.Count > 0)
            {
                PlayAttackSound(attackingSoundClip);
            }
            else return;
        }
    
    public void SetupAudioSources()
        {
            Transform[] transforms = GetComponentsInChildren<Transform>();

            if (weaponAttackName == "") Debug.LogError(name + " missing WeaponAttackName.");
            if (weaponImpactname == "") Debug.LogError(name + " missing WeaponImpactname.");
            if (transforms.Length > 0)
            {
                foreach (Transform t in transforms)
                {
                    if (weaponAttackSource == null && t.name == weaponAttackName)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            weaponAttackSource = audioSource;
                        }
                        else
                        {
                            weaponAttackSource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }

                    if (weaponImpactSource == null && t.name == weaponImpactname)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            weaponImpactSource = audioSource;
                        }
                        else
                        {
                            weaponImpactSource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }
                }
                if (weaponAttackSource == null) Debug.LogError(name + " WeaponAttackSource not set.");
                if (weaponImpactSource == null) Debug.LogError(name + " WeaponImpactSource not set.");

                if (weaponAttackSource != null)
                {
                    weaponAttackSource.playOnAwake = audioOverrite.playOnAwake;
                    weaponAttackSource.spatialBlend = audioOverrite.spacialBlend;
                    weaponAttackSource.maxDistance = audioOverrite.maxDistance;
                    weaponAttackSource.volume = audioOverrite.weaponVolume;
                    
                }

                if (weaponImpactSource != null)
                {
                    weaponImpactSource.playOnAwake = audioOverrite.playOnAwake;
                    weaponImpactSource.spatialBlend = audioOverrite.spacialBlend;
                    weaponImpactSource.maxDistance = audioOverrite.maxDistance;
                    weaponImpactSource.volume = audioOverrite.weaponVolume;

                }
            }
            weaponAttackSource.outputAudioMixerGroup = mixerHandler.effects;
            weaponImpactSource.outputAudioMixerGroup = mixerHandler.effects;

        }

    }
}

