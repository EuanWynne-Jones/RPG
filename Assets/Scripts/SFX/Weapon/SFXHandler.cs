using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class SFXHandler : MonoBehaviour
    {
        public WeaponSFX weaponSFX = null;

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
