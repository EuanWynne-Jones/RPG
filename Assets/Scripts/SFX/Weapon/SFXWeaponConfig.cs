using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "WeaponSFXConfig", menuName = "Weapons/ Make New WeaponSFXConfig", order = 1)]
    public class SFXWeaponConfig : ScriptableObject
    {
        // Start is called before the first frame update
        [field: SerializeField] public AudioClip[] AttackSounds { get; private set; }
        [field: SerializeField] public AudioClip[] AttackImpactSounds { get; private set; }

    }
}
