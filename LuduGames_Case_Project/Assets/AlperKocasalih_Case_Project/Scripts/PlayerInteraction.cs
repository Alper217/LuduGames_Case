using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float m_Distance = 3f;
    [SerializeField] private LayerMask m_LayerMask;
    [SerializeField] private Transform m_Camera;

    [Header("UI Settings")]
    [SerializeField] private TextMeshProUGUI m_InteractionTextUI;
    [SerializeField] private Image m_InteractionProgressImage;
    [SerializeField] private float m_InteractionProgress = 0f;

    private IInteractable m_CurrentTarget;
    private float m_Timer = 0f;

    private void Start()
    {
        // Set camera automatically if not assigned
        if (m_Camera == null) m_Camera = Camera.main.transform;
        if (m_InteractionTextUI != null) m_InteractionTextUI.text = "";
    }

    private void Update()
    {
        Scan();
        HandleInput();
    }

    private void Scan()
    {
        Ray ray = new Ray(m_Camera.position, m_Camera.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, m_Distance, m_LayerMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null && interactable.CanInteract)
            {
                if (m_CurrentTarget != interactable)
                {
                    m_CurrentTarget = interactable;
                    if (m_InteractionTextUI != null) 
                        m_InteractionTextUI.text = m_CurrentTarget.GetInteractionText();
                }
                return;
            }
        }
        if (m_CurrentTarget != null)
        {
            m_CurrentTarget = null;
            if (m_InteractionTextUI != null) m_InteractionTextUI.text = "";
        }
        m_Timer = 0f;
    }

    private void HandleInput()
    {
        if (m_CurrentTarget == null) return;

        if (Input.GetKey(KeyCode.E))
        {
            if (m_CurrentTarget.InteractionDuration <= 0)
            {
                if (Input.GetKeyDown(KeyCode.E)) m_CurrentTarget.Interact();
            }
            else
            {
                m_InteractionProgress += Time.deltaTime;
                m_InteractionProgressImage.fillAmount = m_InteractionProgress / m_CurrentTarget.InteractionDuration;
                m_Timer += Time.deltaTime;
                if (m_Timer >= m_CurrentTarget.InteractionDuration)
                {
                    m_CurrentTarget.Interact();
                    m_Timer = 0f;
                }
            }
        }
        else
        {
            m_InteractionProgress = 0f;
            m_InteractionProgressImage.fillAmount = 0f;
            m_Timer = 0f;
        }
    }
}