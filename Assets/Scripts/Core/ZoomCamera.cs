using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG.Core
{
    public class ZoomCamera : MonoBehaviour
    {
        CinemachineVirtualCamera cinemachineVirtualCamera;
        CinemachineComponentBase componentBase;

        float minCameraDistance = 4f;
        float maxCameraDistance = 9.5f;

        float currentCameraDistance;
        float sensitivity = 3f;
        private void Start()
        {
            cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }

        private void Update()
        {
            if(componentBase == null)
            {
                componentBase = cinemachineVirtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
            }

            if(Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                currentCameraDistance = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
                if(currentCameraDistance > maxCameraDistance)
                {
                    currentCameraDistance = maxCameraDistance;

                    if (componentBase is CinemachineFramingTransposer)
                    {

                        (componentBase as CinemachineFramingTransposer).m_CameraDistance -= maxCameraDistance;
                    }
                }
                else if(currentCameraDistance < maxCameraDistance)
                {
                    currentCameraDistance = minCameraDistance;

                    if (componentBase is CinemachineFramingTransposer)
                    {

                        (componentBase as CinemachineFramingTransposer).m_CameraDistance -= minCameraDistance;
                    }
                }
              

            }
        }



    }
}
