using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Inventories
{
    /// <summary>
    /// A ScriptableObject that represents any item that can be put in an
    /// inventory.
    /// </summary>
    /// <remarks>
    /// In practice, you are likely to use a subclass such as `ActionItem` or
    /// `EquipableItem`.
    /// </remarks>
    [CreateAssetMenu(menuName = ("Inventory/New Item"))]
    public class InventoryItem : ScriptableObject, ISerializationCallbackReceiver    {
        // CONFIG DATA
        [Tooltip("Auto-generated UUID for saving/loading. Clear this field if you want to generate a new one.")]
        [SerializeField] string itemID = null;
        [Header("In-Game Item Details")]
        [Tooltip("Item name to be displayed in UI.")]
        [SerializeField] public string displayName = null;
        [Tooltip("The UI icon to represent this item in the inventory.")]
        [SerializeField] public Sprite icon = null;
        [Tooltip("The Quality of the item")]
        [SerializeField] public EItemQuality Quality;
        [Tooltip("Item description to be displayed in UI.")]
        [SerializeField][TextArea] public string description = null;

        [Header("Pickup settings")]
        [Tooltip("The prefab that should be spawned when this item is dropped.")]
        [SerializeField] public Pickup pickup = null;


        [Header("Item Value")]
        [SerializeField] public int goldValue = 0;
        [Range(0,99)]
        [SerializeField] public int silverValue = 0;
        [Range(0, 99)]
        [SerializeField] public int copperValue = 0;

        [Header("Additional Options")]
        [Tooltip("If true, multiple items of this type can be stacked in the same inventory slot.")]
        [SerializeField] public bool stackable = false;



        // STATE
        static Dictionary<string, InventoryItem> itemLookupCache;

        // PUBLIC

        /// <summary>
        /// Get the inventory item instance from its UUID.
        /// </summary>
        /// <param name="itemID">
        /// String UUID that persists between game instances.
        /// </param>
        /// <returns>
        /// Inventory item instance corresponding to the ID.
        /// </returns>
        public static InventoryItem GetFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, InventoryItem>();
                var itemList = Resources.LoadAll<InventoryItem>("");
                foreach (var item in itemList)
                {
                    if (itemLookupCache.ContainsKey(item.itemID))
                    {
                        Debug.LogError(string.Format("Looks like there's a duplicate GameDevTV.UI.InventorySystem ID for objects: {0} and {1}", itemLookupCache[item.itemID], item));
                        continue;
                    }

                    itemLookupCache[item.itemID] = item;
                }
            }

            if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
            return itemLookupCache[itemID];
        }
        
        /// <summary>
        /// Spawn the pickup gameobject into the world.
        /// </summary>
        /// <param name="position">Where to spawn the pickup.</param>
        /// <param name="number">How many instances of the item does the pickup represent.</param>
        /// <returns>Reference to the pickup object spawned.</returns>
        public Pickup SpawnPickup(Vector3 position, int number)
        {
            var pickup = Instantiate(this.pickup);
            pickup.transform.position = position;
            pickup.Setup(this, number);
            return pickup;
        }

        public Sprite GetIcon()
        {
            return icon;
        }

        public string GetItemID()
        {
            return itemID;
        }

        public bool IsStackable()
        {
            return stackable;
        }
        
        public string GetDisplayName()
        {
            return displayName;
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetItemQuality()
        {
            return Quality.ToString();
        }


        public int GetGoldValue()
        {
            if(goldValue >= 1)
            {
            return goldValue;
            }
            else
            {
                return 0;
            }
        }
        public int GetSilveValue()
        {
            if (silverValue >= 1)
            {
                return silverValue;
            }
            else
            {
                return 0;
            }
        }
        public int GetCopperValue()
        {
            if (copperValue >= 1)
            {
                return copperValue;
            }
            else
            {
                return 0;
            }
        }

        // PRIVATE

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            // Generate and save a new UUID if this is blank.
            if (string.IsNullOrWhiteSpace(itemID))
            {
                itemID = System.Guid.NewGuid().ToString();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            // Require by the ISerializationCallbackReceiver but we don't need
            // to do anything with it.
        }
    }
}
