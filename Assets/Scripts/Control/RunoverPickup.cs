using System.Collections;
using System.Collections.Generic;
using RPG.Inventories;
using RPG.Movement;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    [RequireComponent(typeof(Pickup))]
    public class RunoverPickup : MonoBehaviour, IRaycastable
    {
        Pickup pickup;
        private void Awake()
        {
            pickup = GetComponent<Pickup>();
        }

        public CursorType GetCursorType()
        {
            if (pickup.CanBePickedUp())
            {
                return CursorType.Pickup;
            }
            else
            {
                return CursorType.FullPickup;
            }
        }
        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.tag == "Player")
            {
                TriggerLooting(other.gameObject);
                StartCoroutine(WaitToPickup(0.855f, other.gameObject));
                other.GetComponent<PlayerController>().enabled = true;
            }
        }

        private void TriggerLooting(GameObject player)
        {
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<Animator>().SetTrigger("Loot");
        }

        IEnumerator WaitToPickup(float destroyWaitTime, GameObject player)
        {
            yield return new WaitForSeconds(destroyWaitTime);
            player.GetComponent<Animator>().ResetTrigger("Loot");
            pickup.PickupItem();
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            Mover mover = callingController.GetComponent<Mover>();
            if (!mover) return false;
            if (Input.GetMouseButtonDown(0))
            {
                mover.StartMoveAction(transform.position, 1f);
            }
            return true;
        }  
    }
}