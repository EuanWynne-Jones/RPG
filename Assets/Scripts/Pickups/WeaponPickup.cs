using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;


namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weaponToPickup = null;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
               
                TriggerLooting();
                StartCoroutine(PickupAnimationTime());
                other.GetComponent<Fighter>().EquipWeapon(weaponToPickup);
                Destroy(gameObject);

            }
        }

        IEnumerator PickupAnimationTime()
        {
           
            float pickupWaitTime = 3.5f;
            yield return new WaitForSeconds(pickupWaitTime);
            
            
        }

        private void TriggerLooting()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<ActionSchedueler>().CancelCurrentAction();
            player.GetComponent<Animator>().SetTrigger("Loot");
        }


    }

}
