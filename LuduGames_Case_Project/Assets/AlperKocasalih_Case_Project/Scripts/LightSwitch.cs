using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    [SerializeField] private float m_InteractionDuration = 0f;
    [SerializeField] private bool m_CanInteract = true;
    [SerializeField] private string m_InteractionText = "Press [E] to Switch Light";
    [SerializeField] private List<GameObject> m_Light;

    private bool m_IsLightOn = false;
    public float InteractionDuration { get => m_InteractionDuration; }
    public bool CanInteract { get => m_CanInteract; }
    public string InteractionText { get => m_InteractionText; }

    void Start()
    {
        if(m_Light != null)
        {
            foreach (var light in m_Light)
            {
                light.SetActive(false);
            }
        }
    }

    public void Interact()
    {
       Debug.Log("Light Switch Interacted");
       SwitchLight();
    }
    private void SwitchLight()
    {   
        if(m_Light != null)
        {
            if(m_IsLightOn)
            {
                    foreach (var light in m_Light)
                {
                    light.SetActive(false);
                }
                m_IsLightOn = false;
            }
            else
            {
                foreach (var light in m_Light)
                {
                    light.SetActive(true);
                }
                m_IsLightOn = true;
            }
        }
    }
    public string GetInteractionText() { return InteractionText; }
}
