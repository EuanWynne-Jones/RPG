using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Control;

namespace RPG.Combat
{
    public class WeaponSFX : MonoBehaviour
    {
        [SerializeField] WeaponAudioOverrite audioOverrite = null;

        [Header("WeaponAttack")]
        [SerializeField] private string WeaponAttackName = "Attack";
        [SerializeField] private AudioSource WeaponAttackSource;

        [Header("WeaponImpact")]
        [SerializeField] private string WeaponImpactname = "Impact";
        [SerializeField] private AudioSource WeaponImpactSource;

        PlayerController playerCharacter = null;
        
        private void SetupAudioSources()
        {
            Transform[] transforms = GetComponentsInChildren<Transform>();

            if (WeaponAttackName == "") Debug.LogError(name + " missing VoiceSourceBoneName.");
            if (WeaponImpactname == "") Debug.LogError(name + " missing DeathVoiceSourceBoneName.");
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
                if (WeaponAttackSource == null) Debug.LogError(name + " VoiceSource not set.");
                if (WeaponImpactSource == null) Debug.LogError(name + " DeathVoiceSource not set.");

                if (WeaponAttackSource != null)
                {
                    WeaponAttackSource.spatialBlend = audioOverrite.SpacialBlend;
                    WeaponAttackSource.maxDistance = audioOverrite.MaxDistance;
                    WeaponAttackSource.volume = audioOverrite.WeaponVolume;
                    if (isPlayer)
                    {
                        WeaponAttackSource.maxDistance = audioOverrite.PlayerMaxDistance;
                    }
                    else
                    {
                        WeaponAttackSource.maxDistance = audioOverrite.EnemyMaxDistance;
                    }
                }

                if (WeaponImpactSource != null)
                {
                    WeaponImpactSource.spatialBlend = audioOverrite.SpacialBlend;
                    WeaponImpactSource.maxDistance = audioOverrite.MaxDistance;
                    WeaponImpactSource.volume = audioOverrite.WeaponVolume;
                    if (isPlayer)
                    {
                        WeaponImpactSource.maxDistance = audioOverrite.PlayerMaxDistance;
                    }
                    else
                    {
                        WeaponImpactSource.maxDistance = audioOverrite.EnemyMaxDistance;
                    }
                }
            }

        }
        private bool isPlayer
        {
            get
            {
               PlayerController player = playerCharacter.GetComponent<PlayerController>();
               return player != null;
            }
        }
    }
}

