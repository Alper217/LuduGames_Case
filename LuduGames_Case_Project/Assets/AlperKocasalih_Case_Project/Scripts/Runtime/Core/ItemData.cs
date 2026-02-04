using UnityEngine;

namespace AlperKocasalih_Case_Project.Scripts
{
    /// <summary>
    /// Represents the data for an inventory item.
    /// </summary>
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class ItemData : ScriptableObject
    {
        [Tooltip("The name of the item.")]
        public string ItemName;

        [Tooltip("The icon to display in the UI.")]
        public Sprite ItemIcon;

        [Tooltip("The type of the item.")]
        public ItemType ItemType;
    }

    /// <summary>
    /// Defines the types of items available.
    /// </summary>
    public enum ItemType
    {
        DoorKey,
        ChestKey,
        Other
    }
}
