using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Core.UI.Dragging;
using RPG.Inventories;
using RPG.Control;
using RPG.Core;

namespace RPG.UI.Inventories
{
    /// <summary>
    /// Handles spawning pickups when item dropped into the world.
    /// 
    /// Must be placed on the root canvas where items can be dragged. Will be
    /// called if dropped over empty space. 
    /// </summary>
    public class InventoryDropTarget : MonoBehaviour, IDragDestination<InventoryItem>
    {
        public void AddItems(InventoryItem item, int number)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<ItemDropper>().DropItem(item, number);
            TriggerLooting(player);

        }
        private void TriggerLooting(GameObject player)
        {
            player.GetComponent<ActionSchedueler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<Animator>().SetTrigger("Loot");
            StartCoroutine(PickupAnimationTime(1.7f, player));
        }

        IEnumerator PickupAnimationTime(float pickupWaitTime, GameObject player)
        {
            yield return new WaitForSeconds(pickupWaitTime);
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Animator>().ResetTrigger("Loot");
            player.GetComponent<PlayerController>().enabled = true;


        }
        public int MaxAcceptable(InventoryItem item)
        {
            return int.MaxValue;
        }
    }
}