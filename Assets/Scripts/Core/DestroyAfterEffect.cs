using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        void Update()
        {
            if (!GetComponentInChildren<ParticleSystem>().IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }

}