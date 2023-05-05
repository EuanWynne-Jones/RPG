using RPG.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.UI
{
    public class OutlineHandler : MonoBehaviour
    {
        private Outline outline;

        private void Awake()
        {
            outline = GetComponent<Outline>();
            outline.enabled = false;
        }
        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                outline.enabled = true;
            }
            else outline.enabled = false;
        }



    }
}
