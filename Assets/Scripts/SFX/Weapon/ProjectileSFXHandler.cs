using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class ProjectileSFXHandler: MonoBehaviour
    {
        public ProjectileSFX projectileSFX = null;
        public void PlayLaunching()
        {
            projectileSFX.PlayLaunching();
        }

        public void PlayProjectileImpacting()
        {
            projectileSFX.PlayImpacting();
        }
    }
}
