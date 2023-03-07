using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class SoundscapeSetter : MonoBehaviour
    {
        [SerializeField] public WorldSFXConfig currentWorldSFXConfig;
        WorldSFX worldSFX;
        private void Start()
        {
            worldSFX = FindObjectOfType<WorldSFX>();
            worldSFX.soundscapeSetter = this;
            worldSFX.worldSFXConfig = currentWorldSFXConfig;
            worldSFX.SetupAudioSources();
            worldSFX.worldSFXConfig.SoundtrackTrigger();
        }
    }
}
