using UnityEngine;
using UnityEngine.Events;

namespace AlperKocasalih_Case_Project.Scripts
{
    /// <summary>
    /// Represents a key item that can be picked up.
    /// Adds itself to the inventory upon interaction.
    /// </summary>
    public class Key : InteractableBase
    {
        #region Fields

        [Header("Key Settings")]
        [Tooltip("The data associated with this key item.")]
        [SerializeField] private ItemData m_ItemData;

        [Tooltip("Text to display when looking at the key.")]
        [SerializeField] private string m_InteractionText = "Press [E] to Pick Up Key";

        [Header("Events")]
        [Tooltip("Event triggered when the key is picked up.")]
        public UnityEvent OnKeyPickedUp;

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the interaction text.
        /// </summary>
        public override string GetInteractionText() => m_InteractionText;

        /// <summary>
        /// Handles the interaction (pick up).
        /// Adds the item to inventory and destroys the object on success.
        /// </summary>
        public override void Interact()
        {
            if (m_ItemData != null)
            {
                if (InventoryManager.Instance.AddItem(m_ItemData))
                {
                    Debug.Log($"SUCCESS: Key picked up: {m_ItemData.ItemName}");
                    OnKeyPickedUp?.Invoke();
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("FAIL: Inventory Full! Key NOT picked up.");
                }
            }
            else
            {
                Debug.LogError("Key script: ItemData is missing!");
            }
        }

        #endregion
    }
}