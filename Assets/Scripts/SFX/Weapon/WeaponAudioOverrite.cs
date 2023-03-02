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
        [field: SerializeField] public bool playOnAwake { get; private set; } = false;



    }
}
