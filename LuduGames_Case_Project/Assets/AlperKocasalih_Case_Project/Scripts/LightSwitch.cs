using System.Collections.Generic;
using UnityEngine;

namespace AlperKocasalih_Case_Project.Scripts
{
    /// <summary>
    /// Represents a light switch that toggles one or more lights.
    /// </summary>
    public class LightSwitch : InteractableBase
    {
        #region Fields

        [Header("Light Settings")]
        [Tooltip("List of light objects to toggle.")]
        [SerializeField] private List<GameObject> m_Lights;

        [Tooltip("Interaction text.")]
        [SerializeField] private string m_InteractionText = "Press [E] to Switch Light";

        [Tooltip("Is the light currently on?")]
        private bool m_IsLightOn = false;

        #endregion

        #region Public Methods

        public override string GetInteractionText() => m_InteractionText;

        public override void Interact()
        {
            ToggleLights();
        }

        #endregion

        #region Private Methods

        private void ToggleLights()
        {
            m_IsLightOn = !m_IsLightOn;

            if (m_Lights != null)
            {
                foreach (var light in m_Lights)
                {
                    if (light != null)
                    {
                        light.SetActive(m_IsLightOn);
                    }
                }
            }
        }

        #endregion
    }
}
