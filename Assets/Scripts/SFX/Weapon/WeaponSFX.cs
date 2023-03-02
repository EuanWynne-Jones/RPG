using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat
{
    public class WeaponSFX : MonoBehaviour
    {


        [Header("WeaponAttack")]
        private string WeaponAttackName = "Attack";
        [SerializeField] public AudioSource WeaponAttackSource;
        [SerializeField] public List<AudioClip> AttackSounds;

        [Header("WeaponImpact")]
        private string WeaponImpactname = "Impact";
        [SerializeField] public AudioSource WeaponImpactSource;
        [SerializeField] public List<AudioClip> ImpactSounds;

        [Header("AudioSourceOverrite")]
        [SerializeField] WeaponAudioOverrite audioOverrite = null;


        public void SetupAudioSources()
        {
            Transform[] transforms = GetComponentsInChildren<Transform>();

            if (WeaponAttackName == "") Debug.LogError(name + " missing WeaponAttackName.");
            if (WeaponImpactname == "") Debug.LogError(name + " missing WeaponImpactname.");
            if (transforms.Length > 0)
            {
                foreach (Transform t in transforms)
                {
                    if (WeaponAttackSource == null && t.name == WeaponAttackName)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            WeaponAttackSource = audioSource;
                        }
                        else
                        {
                            WeaponAttackSource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }

                    if (WeaponImpactSource == null && t.name == WeaponImpactname)
                    {
                        if (t.TryGetComponent<AudioSource>(out AudioSource audioSource))
                        {
                            WeaponImpactSource = audioSource;
                        }
                        else
                        {
                            WeaponImpactSource = t.gameObject.AddComponent<AudioSource>();
                        }
                    }
                }
                if (WeaponAttackSource == null) Debug.LogError(name + " WeaponAttackSource not set.");
                if (WeaponImpactSource == null) Debug.LogError(name + " WeaponImpactSource not set.");

                if (WeaponAttackSource != null)
                {
                    WeaponAttackSource.playOnAwake = audioOverrite.playOnAwake;
                    WeaponAttackSource.spatialBlend = audioOverrite.SpacialBlend;
                    WeaponAttackSource.maxDistance = audioOverrite.MaxDistance;
                    WeaponAttackSource.volume = audioOverrite.WeaponVolume;
                    
                }

                if (WeaponImpactSource != null)
                {
                    WeaponImpactSource.playOnAwake = audioOverrite.playOnAwake;
                    WeaponImpactSource.spatialBlend = audioOverrite.SpacialBlend;
                    WeaponImpactSource.maxDistance = audioOverrite.MaxDistance;
                    WeaponImpactSource.volume = audioOverrite.WeaponVolume;

                }
            }

        }

    }
}

