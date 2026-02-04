using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AlperKocasalih_Case_Project.Scripts
{
    /// <summary>
    /// Represents a chest interactable object.
    /// Handles opening, closing, and locking logic with lid rotation.
    /// </summary>
    public class Chest : InteractableBase
    {
        #region Fields

        [Header("Chest Settings")]
        [Tooltip("The lid object to rotate.")]
        [SerializeField] private GameObject m_ChestLid;

        [Tooltip("Is the chest currently open?")]
        [SerializeField] private bool m_IsOpen = false;

        [Tooltip("Is the chest currently locked?")]
        [SerializeField] private bool m_IsLocked = true;

        [Tooltip("Text to display when locked.")]
        [SerializeField] private string m_LockedText = "Chest is Locked (Key Required)";

        [Tooltip("Text to display when unlocked.")]
        [SerializeField] private string m_UnlockedText = "Press and hold [E] to Open Chest";

        [Tooltip("Duration to unlock the chest.")]
        [SerializeField] private float m_UnlockDuration = 2f;

        [Tooltip("Duration to open the chest.")]
        [SerializeField] private float m_OpenDuration = 0f;

        [Tooltip("Speed of the lid rotation.")]
        [SerializeField] private float m_Speed = 2f;

        [Tooltip("Angle to open the lid.")]
        [SerializeField] private float m_OpenAngle = 180f;

        [Header("Key Settings")]
        [Tooltip("The key required to unlock this chest.")]
        [SerializeField] private ItemData m_RequiredKey;

        [Header("Events")]
        [Tooltip("Event triggered when the chest is opened (unlocked).")]
        public UnityEvent OnChestOpened;

        private Quaternion m_ClosedRotation;
        private Quaternion m_OpenRotation;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the interaction duration based on lock state.
        /// </summary>
        public override float InteractionDuration
        {
            get
            {
                if (m_IsLocked)
                {
                    return InventoryManager.Instance.HasChestKey ? m_UnlockDuration : m_OpenDuration;
                }
                return m_OpenDuration;
            }
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            if (m_ChestLid != null)
            {
                m_ClosedRotation = m_ChestLid.transform.localRotation;
                m_OpenRotation = Quaternion.Euler(m_OpenAngle, 0f, 0f);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the interaction text based on lock and open state.
        /// </summary>
        public override string GetInteractionText()
        {
            if (m_IsLocked)
            {
                return InventoryManager.Instance.HasChestKey ? m_UnlockedText : m_LockedText;
            }
            return m_IsOpen ? "Press [E] to Close Chest" : "Press [E] to Open Chest";
        }

        /// <summary>
        /// Handles interaction. Unlocks/Opens the chest.
        /// </summary>
        public override void Interact()
        {
            if (m_IsLocked)
            {
                if (InventoryManager.Instance.HasChestKey)
                {
                    Debug.Log("Unlocking the chest...");
                    m_IsLocked = false;
                    OpenChest();

                    if (m_RequiredKey != null)
                    {
                        InventoryManager.Instance.RemoveItem(m_RequiredKey);
                    }
                }
                else
                {
                    Debug.Log("Locked! Cannot open.");
                }
                return;
            }
            Debug.Log("Chest opened!");
            OpenChest();
        }

        /// <summary>
        /// Locks the chest and closes it.
        /// Can be triggered by external events (e.g., picking up a key).
        /// </summary>
        public void LockChest()
        {
            StopAllCoroutines();
            m_IsLocked = true;
            m_IsOpen = false;
            
            // Assuming we want to close it visually when forcing lock
            Quaternion target = m_ClosedRotation;

            StartCoroutine(RotateChest(target));
        }

        #endregion

        #region Private Methods

        private void OpenChest()
        {
            StopAllCoroutines();
            m_IsOpen = !m_IsOpen;

            if (m_IsOpen)
            {
                OnChestOpened?.Invoke();
            }

            Quaternion target = m_IsOpen ? m_OpenRotation : m_ClosedRotation;
            StartCoroutine(RotateChest(target));
        }

        private IEnumerator RotateChest(Quaternion targetRotation)
        {
            while (Quaternion.Angle(m_ChestLid.transform.localRotation, targetRotation) > 0.1f)
            {
                m_ChestLid.transform.localRotation = Quaternion.Slerp(m_ChestLid.transform.localRotation, targetRotation, Time.deltaTime * m_Speed);
                yield return null;
            }
            m_ChestLid.transform.localRotation = targetRotation;
        }

        #endregion
    }
}
