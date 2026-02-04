using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AlperKocasalih_Case_Project.Scripts
{
    /// <summary>
    /// Handles player interaction with the world using SphereCast.
    /// Manages UI feedback for interactions.
    /// </summary>
    public class PlayerInteraction : MonoBehaviour
    {
        #region Fields

        [Header("Detection Settings")]
        [Tooltip("Max distance to detect interactable objects.")]
        [SerializeField] private float m_Distance = 3f;

        [Tooltip("Radius of the sphere cast for detection.")]
        [SerializeField] private float m_SphereCastRadius = 0.5f;

        [Tooltip("Layer mask to filter interactable objects.")]
        [SerializeField] private LayerMask m_LayerMask;

        [Header("UI Settings")]
        [Tooltip("Text element to display interaction prompt.")]
        [SerializeField] private TextMeshProUGUI m_InteractionText;

        [Tooltip("Progress bar image for hold interactions.")]
        [SerializeField] private Image m_ProgressBar;

        [Tooltip("Transform of the camera.")]
        [SerializeField] private Transform m_Camera;

        private IInteractable m_CurrentTarget;
        private float m_Timer = 0f;

        #endregion

        #region Unity Methods

        private void Start()
        {
             if (m_Camera == null) m_Camera = Camera.main.transform;
             if(m_InteractionText != null) m_InteractionText.gameObject.SetActive(false);
             if(m_ProgressBar != null) m_ProgressBar.gameObject.SetActive(false);
        }

        private void Update()
        {
            Scan();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Scans for interactable objects using SphereCast.
        /// Handles input processing and UI updates.
        /// </summary>
        private void Scan()
        {
            if (Physics.SphereCast(m_Camera.position, m_SphereCastRadius, m_Camera.forward, out RaycastHit hit, m_Distance, m_LayerMask))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null && interactable.CanInteract)
                {
                    m_CurrentTarget = interactable;
                    m_InteractionText.text = interactable.InteractionText;
                    m_InteractionText.gameObject.SetActive(true);

                    HandleInput(interactable);
                }
                else
                {
                    ClearInteraction();
                }
            }
            else
            {
                ClearInteraction();
            }
        }

        /// <summary>
        /// Handles the player input for interaction.
        /// </summary>
        /// <param name="interactable">The target interactable object.</param>
        private void HandleInput(IInteractable interactable)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (interactable.InteractionDuration > 0f)
                {
                    m_Timer += Time.deltaTime;
                    
                    // Update progress bar
                    if (m_ProgressBar != null)
                    {
                        m_ProgressBar.fillAmount = m_Timer / interactable.InteractionDuration;
                        m_ProgressBar.gameObject.SetActive(true);
                    }

                    if (m_Timer >= interactable.InteractionDuration)
                    {
                        interactable.Interact();
                        m_Timer = 0f; // Reset timer after success
                        if (m_ProgressBar != null) m_ProgressBar.fillAmount = 0f;
                    }
                }
                else
                {
                    // Instant interaction (ensure single trigger)
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        interactable.Interact();
                    }
                }
            }
            else
            {
                // Reset timer if key is released
                m_Timer = 0f;
                if (m_ProgressBar != null)
                {
                    m_ProgressBar.fillAmount = 0f;
                    m_ProgressBar.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>
        /// Clears the current interaction state and UI.
        /// </summary>
        private void ClearInteraction()
        {
            m_CurrentTarget = null;
            if(m_InteractionText != null) m_InteractionText.gameObject.SetActive(false);
            m_Timer = 0f;
            if (m_ProgressBar != null)
            {
                m_ProgressBar.gameObject.SetActive(false);
                m_ProgressBar.fillAmount = 0f;
            }
        }

        #endregion
    }
}