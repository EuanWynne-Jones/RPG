using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "AudioOverrite", menuName = "Weapons/ Make New AudioOverrite", order = 1)]
    public class WeaponAudioOverrite : ScriptableObject
    {
        [field: SerializeField, Header("Audio Overrides")] public float spacialBlend { get; private set; } = 1;
        [field: SerializeField] public float maxDistance { get; private set; } = 100f;
        [field: SerializeField] public float weaponVolume { get; private set; } = 1;
        [field: SerializeField] public bool playOnAwake { get; private set; } = false;



    }
}
