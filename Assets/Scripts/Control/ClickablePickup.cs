using UnityEngine;
using RPG.Inventories;
using RPG.Control;
using RPG.Movement;
using System.Collections;
using RPG.Core;
using RPG.Attributes;
using RPG.Combat;

namespace RPG.Control
{
    [RequireComponent(typeof(Pickup))]
    public class ClickablePickup : MonoBehaviour, IRaycastable
    {
        Pickup pickup;
        PlayerController player;

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>();
            pickup = GetComponent<Pickup>();
        }
        private void Update()
        {
            float dist = GetDistancetoObject();
            if (dist <= .5f && pickup.pickupIntention == true && player.GetComponent<Health>().isInSpiritRealm == false)
            {
                player.GetComponent<ActionSchedueler>().CancelCurrentAction();
                TriggerLooting(player.gameObject);
                pickup.PickupItem();
                player.GetComponent<SFXHandler>().PlayPickupSFX();
                StartCoroutine(WaitForAnim(1.6f));
                player.GetComponent<PlayerController>().enabled = true;
            }
        }

        private float GetDistancetoObject()
        {
            return Vector3.Distance(player.transform.position, transform.position);
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

        IEnumerator WaitForAnim(float WaitTime)
        {
            yield return new WaitForSeconds(WaitTime);
            player.GetComponent<Animator>().ResetTrigger("Loot");
            
        }

        private void TriggerLooting(GameObject player)
        {
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<Animator>().SetTrigger("Loot");
            
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