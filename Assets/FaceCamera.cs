using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class FaceCamera : MonoBehaviour
    {
        private Camera mainCamera;

        void Start()
        {
            // Find the main camera in the scene
            mainCamera = Camera.main;
        }

        void Update()
        {
            // Calculate the direction to the camera on the Y-axis only
            Vector3 direction = mainCamera.transform.position - transform.position;
            direction.y = 0;

            // Rotate the object to face the camera direction
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}

