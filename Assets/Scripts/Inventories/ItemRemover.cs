using RPG.Utils;
using UnityEngine;

namespace RPG.Inventories
{
    public class ItemRemover : MonoBehaviour
    {
        [SerializeField] private InventoryItem itemToRemove;
        [SerializeField] private int number;

        public void RemoveItem(InventoryItem item)
        {
            Inventory.GetPlayerInventory().RemoveItem(item, number);
        }
    }
}