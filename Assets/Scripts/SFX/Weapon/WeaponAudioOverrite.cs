using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "AudioOverrite", menuName = "Weapons/ Make New AudioOverrite", order = 1)]
    public class WeaponAudioOverrite : ScriptableObject
    {
        [field: SerializeField, Header("Weapon Audio Overrides")] public float SpacialBlend { get; private set; } = 1;
        [field: SerializeField] public float MaxDistance { get; private set; } = 100f;
        [field: SerializeField] public float WeaponVolume { get; private set; } = 1;
        [field: SerializeField] public float PlayerMaxDistance { get; private set; } = 50f;
        [field: SerializeField] public float EnemyMaxDistance { get; private set; } = 50f;
    }
}
