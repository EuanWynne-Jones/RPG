using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG.Dialogue
{
    public class CameraTransition : MonoBehaviour
    {
        public CinemachineVirtualCamera[] cameras;

        [SerializeField] public CinemachineVirtualCamera mainCamera;
        [SerializeField] public CinemachineVirtualCamera dialogueCamera;

        public CinemachineVirtualCamera startCamera;
        private CinemachineVirtualCamera currentCam;


        private void Start()
        {
            currentCam = startCamera;
            for (int i = 0; i< cameras.Length; i++)
			{
                if (cameras[i] == currentCam)
                {
                            cameras[i].Priority = 20;
                }
                else
                {
                    cameras[i].Priority = 10;
                }

			}
            
        }

        public void SwitchCamera(CinemachineVirtualCamera newCam)
        {
            currentCam = newCam;

            currentCam.Priority = 20;
            for (int i = 0; i < cameras.Length; i++)
            {
                if (cameras[i] == currentCam)
                {
                    cameras[i].Priority = 20;
                }
                else
                {
                    cameras[i].Priority = 10;
                }

            }
        }
    }
}
