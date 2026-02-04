using System.Collections;
using UnityEngine;

namespace AlperKocasalih_Case_Project.Scripts
{
    /// <summary>
    /// Represents a door interactable object.
    /// Handles locking, opening, and closing logic.
    /// </summary>
    public class Door : InteractableBase
    {
        #region Fields

        [Header("Door Settings")]
        [Tooltip("Is the door currently open?")]
        [SerializeField] private bool m_IsOpen = false;

        [Tooltip("The rotation when the door is closed.")]
        [SerializeField] private Quaternion m_ClosedRotation;

        [Tooltip("The rotation when the door is open.")]
        [SerializeField] private Quaternion m_OpenRotation;

        [Tooltip("The angle to rotate when opening.")]
        [SerializeField] private float m_OpenAngle = 90f;

        [Tooltip("Speed of the door rotation.")]
        [SerializeField] private float m_Speed = 2f;

        [Tooltip("Duration of the door opening/closing animation.")]
        [SerializeField] private float m_Duration = 1f;

        [Header("Lock Settings")]
        [Tooltip("Is the door locked?")]
        [SerializeField] private bool m_IsLocked = true;

        [Tooltip("Text to display when the door is locked.")]
        [SerializeField] private string m_LockedText = "Door is Locked (Key Required)";

        [Tooltip("Duration required to unlock the door.")]
        [SerializeField] private float m_UnlockDuration = 2f;

        [Tooltip("The key required to unlock this door.")]
        [SerializeField] private ItemData m_RequiredKey;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the interaction duration.
        /// Returns unlock duration if locked and key is present, otherwise 0 or open duration.
        /// </summary>
        public override float InteractionDuration
        {
            get
            {
                // 1. Kilitli mi?
                if (m_IsLocked)
                {
                    // Anahtar varsa süre gönder, yoksa anında tepki ver
                    return InventoryManager.Instance.HasDoorKey ? m_UnlockDuration : 0f;
                }

                // 2. Kilit açıldıktan sonra kapı anında (0 sn) açılsın
                return 0f;
            }
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            m_ClosedRotation = transform.rotation;
            m_OpenRotation = m_ClosedRotation * Quaternion.Euler(0, m_OpenAngle, 0);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handles the interaction logic for the door.
        /// Unlocks if locked and key is present, otherwise toggles open/close state.
        /// </summary>
        public override void Interact()
        {
            if (m_IsLocked)
            {
                if (InventoryManager.Instance.HasDoorKey)
                {
                    Debug.Log("Unlocking the door...");
                    m_IsLocked = false; // Kilidi aç

                    if (m_RequiredKey != null)
                    {
                        InventoryManager.Instance.RemoveItem(m_RequiredKey);
                    }

                    ExecuteRotation();
                }
                else
                {
                    Debug.Log("Locked! You need a key.");
                }
                return;
            }

            ExecuteRotation();
        }

        /// <summary>
        /// Gets the interaction text to display.
        /// </summary>
        /// <returns>Interaction text based on state.</returns>
        public override string GetInteractionText()
        {
            if (m_IsLocked)
            {
                return InventoryManager.Instance.HasDoorKey ? "Press [E] to Unlock Door" : m_LockedText;
            }
            return m_IsOpen ? "Press [E] to Close Door" : "Press [E] to Open Door";
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Toggles the door state and starts rotation coroutine.
        /// </summary>
        private void ExecuteRotation()
        {
            StopAllCoroutines();
            m_IsOpen = !m_IsOpen;
            Quaternion target = m_IsOpen ? m_OpenRotation : m_ClosedRotation;
            StartCoroutine(RotateDoor(target));
        }

        private IEnumerator RotateDoor(Quaternion targetRotation)
        {
            while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * m_Speed);
                yield return null;
            }
            transform.rotation = targetRotation;
        }

        #endregion
    }
}