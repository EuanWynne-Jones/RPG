using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Movement 
{
    public class NPCDirector : MonoBehaviour
    {
        private Mover mover;

        private void Awake()
        {
            mover = GetComponent<Mover>();
        }

        public void MoveToLocation(Transform target)
        {
            Vector3 targetVector = new Vector3(target.position.x, target.position.y, target.position.z);
            mover.MoveTo(targetVector, 1f);
        }


    }

}