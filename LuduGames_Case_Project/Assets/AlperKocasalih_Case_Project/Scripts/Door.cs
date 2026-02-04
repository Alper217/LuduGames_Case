using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private float m_InteractionDuration = 0f;
    [SerializeField] private bool m_CanInteract = true;
    [SerializeField] private string m_InteractionText = "Press [E] to Interact with Door";

    [SerializeField] private float m_OpenAngle = 90f; 
    [SerializeField] private float m_Speed = 2f;      
    private bool m_IsOpen = false;
    private Quaternion m_ClosedRotation;
    private Quaternion m_OpenRotation;

    public float InteractionDuration { get => m_InteractionDuration; }
    public bool CanInteract { get => m_CanInteract; }
    public string InteractionText { get => m_InteractionText; }

    private void Start()
    {
        m_ClosedRotation = transform.rotation;
        m_OpenRotation = m_ClosedRotation * Quaternion.Euler(0, m_OpenAngle, 0);
    }
    public void Interact()
    {
         Debug.Log("[Door] Interacted - Sliding the door...");
         StopAllCoroutines(); 
         m_IsOpen = !m_IsOpen;
        
        Quaternion target = m_IsOpen ? m_OpenRotation : m_ClosedRotation;
        StartCoroutine(RotateDoor(target));
    }
     private IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            // Slerp: Mevcut rotasyondan hedefe pürüzsüz geçiş yapar
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * m_Speed);
            yield return null; // Bir sonraki frame'e kadar bekle
        }
        // Tam hedefe eşitle (küsuratları temizlemek için)
        transform.rotation = targetRotation;
    }
    public string GetInteractionText() { return InteractionText; }

    
}