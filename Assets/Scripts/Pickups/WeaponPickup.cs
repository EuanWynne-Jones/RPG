using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Control;
using RPG.Movement;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        
        [SerializeField] Weapon weaponToPickup = null;
        [SerializeField] AnimatorOverrideController leftHandPickupOverrite = null;

        Weapon currentWeapon;
        Weapon oldWeapon;

        [HideInInspector]
        public GameObject droppedInstantiation = null;

        bool hasDropped;
        public bool canPickup = true;

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.gameObject.tag == "Player")
            {
                if (canPickup)
                {
                    Pickup(other.GetComponent<Fighter>());
                }

            }
        
        }

        private void Pickup(Fighter fighter)
        {
            oldWeapon = GetPlayersCurrentWeapon(currentWeapon);
            fighter.gameObject.GetComponent<ActionSchedueler>().CancelCurrentAction();
            LookAtObject(fighter.gameObject);
            TriggerLooting(fighter.gameObject);
            StartCoroutine(WaitToDestoryOldWeapon(0.5f, fighter.gameObject));
            StartCoroutine(PickupAnimationTime(0.855f, fighter.gameObject));
            hasDropped = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if (hasDropped)
            {
                canPickup = true;  
            }
        }
        private void DropCurrentWeapon()
        {
            if (!hasDropped)
            {
            GetPlayersCurrentWeapon(currentWeapon);
            if (oldWeapon.dropPrefab == null) return;
            GameObject Instantiated = Instantiate(oldWeapon.dropPrefab, GetPlayerDropPostion(), Quaternion.identity);
            Instantiated.GetComponent<WeaponPickup>().canPickup = false;
            droppedInstantiation = Instantiated;
            droppedInstantiation.GetComponent<WeaponPickup>().hasDropped = true;
            


            }

        }

    

        private Weapon GetPlayersCurrentWeapon(Weapon weapon)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            currentWeapon =  player.GetComponent<Fighter>().currentWeapon.value;
            return weapon = currentWeapon;
        } 
        
        private Vector3 GetPlayerDropPostion()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 playerPosition = player.transform.position; 
            return playerPosition; 
        }


        IEnumerator PickupAnimationTime(float pickupWaitTime, GameObject player)
        {
           
            yield return new WaitForSeconds(pickupWaitTime);
            player = GameObject.FindGameObjectWithTag("Player");

            player.GetComponent<Fighter>().EquipWeapon(weaponToPickup);
            Destroy(gameObject);
            DropCurrentWeapon();


        }

        IEnumerator WaitToDestoryOldWeapon(float destroyWaitTime, GameObject player)
        {
            yield return new WaitForSeconds(destroyWaitTime);
            player = GameObject.FindGameObjectWithTag("Player");
            GetPlayersCurrentWeapon(currentWeapon);
            currentWeapon.DestroyOldWeapon(player.GetComponent<Fighter>().rightHandTrasform, player.GetComponent<Fighter>().leftHandTrasform);

        }


        
        

        

        private void TriggerLooting(GameObject player)
        {
            if (weaponToPickup.isRightHanded == false && leftHandPickupOverrite != null)
            {
                player.GetComponent<Animator>().runtimeAnimatorController = leftHandPickupOverrite;
                player.GetComponent<Animator>().SetTrigger("Loot");
            }
            else if (leftHandPickupOverrite == null)
            {
            player.GetComponent<Animator>().SetTrigger("Loot");
            }
        }
        private void LookAtObject(GameObject player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.LookAt(this.transform.position);
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<Mover>().MoveTo(gameObject.transform.position, 1);
                
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }

}
