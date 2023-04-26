using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;

namespace RPG.Cinimatics
{
    public class CinimaticTrigger : MonoBehaviour
    {
  
        bool hasTriggered = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == ("Player") && !hasTriggered)
            {
                hasTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }



    }
}
