using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace AlperKocasalih_Case_Project.Scripts
{
    /// <summary>
    /// Represents a bed interactable object.
    /// Handles sleeping logic (Save Game) and screen fading effects.
    /// </summary>
    public class Bed : InteractableBase
    {
        #region Fields

        [Header("Bed Settings")]
        [Tooltip("Duration required to hold the interaction key.")]
        [SerializeField] private float m_InteractionDuration = 2f;

        [Tooltip("Text to display for interaction.")]
        [SerializeField] private string m_InteractionText = "Press and hold [E] to Sleep (Save Game)";

        [Header("Fade Settings")]
        [Tooltip("Image component used for the fade effect.")]
        [SerializeField] private Image m_FadeImage;

        [Tooltip("Duration of the fade in/out effect.")]
        [SerializeField] private float m_FadeDuration = 1.0f;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the required interaction duration.
        /// </summary>
        public override float InteractionDuration => m_InteractionDuration;

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the interaction text.
        /// </summary>
        public override string GetInteractionText() => m_InteractionText;

        /// <summary>
        /// Handles the interaction (Sleep/Save).
        /// Triggers the save logic and fade sequence.
        /// </summary>
        public override void Interact()
        {
            Debug.Log("Interacting with Bed...");
            SaveGame();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Simulates saving the game and starts the fade sequence.
        /// </summary>
        private void SaveGame()
        {
            Debug.Log("Game Saved!");
            // SaveGameData(); // Placeholder for future implementation

            if (m_FadeImage != null)
            {
                StartCoroutine(FadeSequence());
            }
            else
            {
                Debug.LogWarning("Bed: Fade Image is not assigned! Cannot fade screen.");
            }
        }

        private IEnumerator FadeSequence()
        {
            // 1. Fade to Black
            yield return StartCoroutine(Fade(1f, m_FadeDuration));

            // 2. Wait while black (Sleep simulation)
            yield return new WaitForSeconds(1f);

            // 3. Fade to Transparent
            yield return StartCoroutine(Fade(0f, m_FadeDuration));
        }

        private IEnumerator Fade(float targetAlpha, float duration)
        {
            float startAlpha = m_FadeImage.color.a;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);

                Color color = m_FadeImage.color;
                color.a = newAlpha;
                m_FadeImage.color = color;

                yield return null;
            }

            // Ensure final value is set exact
            Color finalColor = m_FadeImage.color;
            finalColor.a = targetAlpha;
            m_FadeImage.color = finalColor;
        }

        #endregion
    }
}
