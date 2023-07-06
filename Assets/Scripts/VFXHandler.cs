using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.VFX
{
    public class VFXHandler : MonoBehaviour
    {
        [Header("Footsteps")]
        public ParticleSystem FootTrail;

        public Transform leftFootPOS;
        public Transform rightFootPOS;

        public Transform leftFootVFX;
        public Transform rightFootVFX;

        private ParticleSystem leftPS;
        private ParticleSystem rightPS;

        bool isLeft = true;

        private void Awake()
        {
            leftPS = leftFootVFX.GetComponentInChildren<ParticleSystem>();
            rightPS = rightFootVFX.GetComponentInChildren<ParticleSystem>();
        }
        public void PlayFootstepVFX()
        {
            if (isLeft == true)
            {
                leftFootVFX.position = leftFootPOS.position;
                leftPS.Play();
                isLeft = false;
            }
            else
            {
                rightFootVFX.position = rightFootPOS.position;
                rightPS.Play();
                isLeft = true;
            }
        }
    }
}
