using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
using RPG.Core;
using RPG.Control;

namespace RPG.Combat
{
    public class ResurrectPlayer : MonoBehaviour
    {
        Resurrect resurrect = null;
        private void Start()
        {
        resurrect = FindObjectOfType<Resurrect>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                FindObjectOfType<Resurrect>().reviveUIButton.SetActive(true);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                FindObjectOfType<Resurrect>().reviveUIButton.SetActive(false);
            }
        }

    }
}
