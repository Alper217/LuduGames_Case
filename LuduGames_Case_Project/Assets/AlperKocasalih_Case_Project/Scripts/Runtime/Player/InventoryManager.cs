using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AlperKocasalih_Case_Project.Scripts
{
    /// <summary>
    /// Manages the player's inventory and UI.
    /// Implements a singleton pattern for easy access.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        #region Fields

        [Header("UI References")]
        [Tooltip("UI Images representing inventory slots.")]
        [SerializeField] private Image[] m_InventorySlots;

        [Header("Data")]
        [Tooltip("List of items currently in the inventory.")]
        [SerializeField] private List<ItemData> m_Items = new List<ItemData>();

        private static InventoryManager s_Instance;

        #endregion

        #region Properties

        /// <summary>
        /// Singleton instance of the InventoryManager.
        /// </summary>
        public static InventoryManager Instance
        {
            get { return s_Instance; }
            private set { s_Instance = value; }
        }

        /// <summary>
        /// Checks if the inventory contains a Chest Key.
        /// </summary>
        public bool HasChestKey => HasItemType(ItemType.ChestKey);

        /// <summary>
        /// Checks if the inventory contains a Door Key.
        /// </summary>
        public bool HasDoorKey => HasItemType(ItemType.DoorKey);

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this;
                m_Items.Clear();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            UpdateInventoryUI();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an item to the inventory if space is available.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>True if the item was added, false if inventory is full.</returns>
        public bool AddItem(ItemData item)
        {
            if (m_Items.Count >= m_InventorySlots.Length)
            {
                Debug.Log("Inventory Full!");
                return false;
            }

            m_Items.Add(item);
            UpdateInventoryUI();
            return true;
        }

        /// <summary>
        /// Removes an item from the inventory.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public void RemoveItem(ItemData item)
        {
            if (m_Items.Contains(item))
            {
                m_Items.Remove(item);
                UpdateInventoryUI();
            }
        }

        /// <summary>
        /// Updates the inventory UI elements based on the current items.
        /// </summary>
        public void UpdateInventoryUI()
        {
            for (int i = 0; i < m_InventorySlots.Length; i++)
            {
                if (i < m_Items.Count)
                {
                    m_InventorySlots[i].sprite = m_Items[i].ItemIcon;
                    m_InventorySlots[i].enabled = true;
                }
                else
                {
                    m_InventorySlots[i].enabled = false;
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Helper method to check if the inventory contains an item of a specific type.
        /// </summary>
        /// <param name="type">The ItemType to check for.</param>
        /// <returns>True if found, otherwise false.</returns>
        private bool HasItemType(ItemType type)
        {
            foreach (var item in m_Items)
            {
                if (item.ItemType == type) return true;
            }
            return false;
        }

        #endregion
    }
}
