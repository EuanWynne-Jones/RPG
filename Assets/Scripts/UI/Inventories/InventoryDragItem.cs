using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Inventories;
using RPG.Core.UI.Dragging;
using UnityEngine.EventSystems;

namespace RPG.UI.Inventories
{
    /// <summary>
    /// To be placed on icons representing the item in a slot. Allows the item
    /// to be dragged into other slots.
    /// </summary>
    public class InventoryDragItem : DragItem<InventoryItem>, IPointerClickHandler
    {
        private const float DOUBLE_CLICK_TIME = 0.3f;
        private float lastClickTime;

        public void OnPointerClick(PointerEventData eventData)
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            // Double click ! We wil try to swap between the equipement and inventory.
            if (timeSinceLastClick <= DOUBLE_CLICK_TIME)
            {
                EquipableItem removedSourceItem = source.GetItem() as EquipableItem;
                // If not equipable, don't bother.
                if (removedSourceItem == null) return;

                IDragContainer<InventoryItem> destination = null;
                if (source is InventorySlotUI)
                {
                    // We clicked on an InventorySlot. Lets try to find a corresponding EquipementSlot.
                    IEnumerable<EquipmentSlotUI> equipementSlots = FindObjectsOfType<EquipmentSlotUI>();
                    foreach (EquipmentSlotUI equipementSlot in equipementSlots)
                    {
                        if (equipementSlot.GetEquipLocation() == removedSourceItem.GetAllowedEquipLocation())
                        {
                            destination = equipementSlot;
                            break;
                        }
                    }
                }

                if (source is EquipmentSlotUI)
                {
                    // We clicked on an EquipementSlot. Lets try to find a empty InventorySlot.
                    IEnumerable<InventorySlotUI> inventorySlots = FindObjectsOfType<InventorySlotUI>();

                    foreach (InventorySlotUI inventorySlot in inventorySlots)
                    {
                        if (inventorySlot.GetItem() == null)
                        {
                            // we find an empty slot.
                            destination = inventorySlot;
                            break;
                        }
                    }
                }

                // We find a candidate slot for swaping.
                if (destination != null)
                {
                    DropItemIntoContainer(destination);
                }
            }
            lastClickTime = Time.time;
        }
    }
}
