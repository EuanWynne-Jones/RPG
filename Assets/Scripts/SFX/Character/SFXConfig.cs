using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    [CreateAssetMenu(fileName = "SFXConfig", menuName = "SFX/ Make New Config", order = 0)]
    public class SFXConfig : ScriptableObject
    {

        [field: SerializeField, Header("Character Audio Overrides")] public float SpacialBlend { get; private set; } = 1;
        [field: SerializeField] public float MaxDistance { get; private set; } = 100f;
        [field: SerializeField] public float CharacterVolume { get; private set; } = 1;
        [field: SerializeField] public float FootstepVolume { get; private set; } = 0.15f;
        [field: SerializeField] public float PlayerMaxDistance { get; private set; } = 50f;
        [field: SerializeField] public float EnemyMaxDistance { get; private set; } = 50f;

    }
}
