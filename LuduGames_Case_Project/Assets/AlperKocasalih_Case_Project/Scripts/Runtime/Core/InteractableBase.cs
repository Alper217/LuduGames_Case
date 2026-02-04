using UnityEngine;

namespace AlperKocasalih_Case_Project.Scripts
{
    /// <summary>
    /// Base class for all interactable objects.
    /// Implements the IInteractable interface and provides common functionality.
    /// </summary>
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("Base Settings")]
        [Tooltip("Text to display when the player looks at this object.")]
        [SerializeField] protected string m_BaseInteractionText = "Interact";

        #endregion

        #region Properties

        /// <summary>
        /// Duration required to hold the interaction key.
        /// Returns 0 for instant interactions.
        /// </summary>
        public virtual float InteractionDuration => 0f;

        /// <summary>
        /// Determines if the object is currently interactable.
        /// </summary>
        public virtual bool CanInteract => true;

        /// <summary>
        /// Gets the text to display for interaction.
        /// </summary>
        public string InteractionText => GetInteractionText();

        #endregion

        #region Public Methods

        /// <summary>
        /// Performs the interaction logic.
        /// Must be implemented by derived classes.
        /// </summary>
        public abstract void Interact();

        /// <summary>
        /// Retrieves the interaction text.
        /// Can be overridden to provide dynamic text.
        /// </summary>
        /// <returns>The interaction display text.</returns>
        public virtual string GetInteractionText()
        {
            return m_BaseInteractionText;
        }

        #endregion
    }
}
