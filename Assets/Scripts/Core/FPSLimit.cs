using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class FPSLimit : MonoBehaviour
    {
        void Awake()
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 30;
        }
    }
}

