using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class SFXHandler : MonoBehaviour
    {
        public WeaponSFX weaponSFX = null;
        public AudioSource PickupSFX = null;

        private void Awake()
        {

            PickupSFX = GetComponent<AudioSource>();
        }

        public void PlayPickupSFX()
        {
            PickupSFX.Play();
        }

        public void PlayAttacking()
        {
            weaponSFX.PlayAttacking();
        }

        public void PlayImpacting()
        {
            weaponSFX.PlayImpacting();
        }

    }
}
