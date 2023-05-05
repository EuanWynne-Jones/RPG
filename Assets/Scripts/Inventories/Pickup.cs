using System.Collections;
using UnityEngine;

namespace RPG.Inventories
{
    /// <summary>
    /// To be placed at the root of a Pickup prefab. Contains the data about the
    /// pickup such as the type of item and the number.
    /// </summary>
    public class Pickup : MonoBehaviour
    {

        [SerializeField] float canPickupTime = 5f;
        // STATE
        [HideInInspector]
        public InventoryItem item;

        public bool ableToPickup = false;
        public bool pickupIntention = false;
        int number = 1;

        // CACHED REFERENCE
        Inventory inventory;

        // LIFECYCLE METHODS

        private void Awake()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            inventory = player.GetComponent<Inventory>();
            //StartCoroutine(ChangeHasJustDropped(canPickupTime));
        }


        private IEnumerator ChangeHasJustDropped(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            ableToPickup = true;
        }
        // PUBLIC

        /// <summary>
        /// Set the vital data after creating the prefab.
        /// </summary>
        /// <param name="item">The type of item this prefab represents.</param>
        /// <param name="number">The number of items represented.</param>
        public void Setup(InventoryItem item, int number)
        {
            this.item = item;
            if (!item.IsStackable())
            {
                number = 1;
            }
            this.number = number;
        }
        public InventoryItem GetItem()
        {
            return item;
        }

        public int GetNumber()
        {
            return number;
        }

        public void PickupItem()
        {
            bool foundSlot = inventory.AddToFirstEmptySlot(item, number);
            if (foundSlot)
            {
                Destroy(gameObject);
            }
        }

        public bool CanBePickedUp()
        {
            return inventory.HasSpaceFor(item);
        }
    }
}