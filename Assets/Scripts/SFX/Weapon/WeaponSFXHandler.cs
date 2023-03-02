using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponSFXHandler : MonoBehaviour
    {
        public WeaponSFX weaponSFX = null;

        private void Start()
        {
            weaponSFX = GetComponentInChildren<WeaponSFX>();
            weaponSFX.SetupAudioSources();
        }


        private AudioClip AttackingSoundClip
        {
            get
            {
                return weaponSFX.AttackSounds[Random.Range(0, weaponSFX.AttackSounds.Count)];

            }
        }
        private AudioClip ImpactingSoundClip
        {
            get
            {
                return weaponSFX.ImpactSounds[Random.Range(0, weaponSFX.ImpactSounds.Count)];
            }
        }

        private void PlayAttackSound(AudioClip audioClip)
        {
            if (weaponSFX.WeaponAttackSource != null && audioClip != null)
            {
                if (weaponSFX.WeaponAttackSource.isPlaying) weaponSFX.WeaponAttackSource.Stop();
                weaponSFX.WeaponAttackSource.clip = audioClip;
                weaponSFX.WeaponAttackSource.Play();
            }
        }

        private void PlayImpactSound(AudioClip audioClip)
        {
            if (weaponSFX.WeaponImpactSource != null && audioClip != null)
            {
                if (weaponSFX.WeaponImpactSource.isPlaying) weaponSFX.WeaponImpactSource.Stop();
                weaponSFX.WeaponImpactSource.clip = audioClip;
                weaponSFX.WeaponImpactSource.Play();
            }
        }

        public void PlayImpacting()
        {

            if (weaponSFX.ImpactSounds.Count > 0)
            {
                PlayImpactSound(ImpactingSoundClip);
            }
        }
        public void PlayAttacking()
        {
            if (weaponSFX.AttackSounds.Count > 0)
            {
                PlayAttackSound(AttackingSoundClip);
            }
        }
    }
}
