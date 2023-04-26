using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.UI
{
    public class LookAtCamera : MonoBehaviour
    {
        void LateUpdate()
        {
            Camera cam = Camera.main;
            transform.LookAt(transform.position + cam.transform.rotation * Vector3.back, cam.transform.rotation * Vector3.up);
        }
    }

}

