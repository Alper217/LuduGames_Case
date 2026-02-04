using UnityEngine;
using System.Collections;

// ARTIK IInteractable yerine InteractableBase'den miras alıyoruz.
public class Door : InteractableBase
{
    [Header("Basic Settings")]
    [SerializeField] private float m_Speed = 2f;
    [SerializeField] private float m_OpenAngle = 90f;
    [SerializeField] private float m_Duration = 0f; // Açma süresi (basılı tutma)

    [Header("Lock Settings")]
    [SerializeField] private bool m_IsLocked = true; // Kapı kilitli başlasın mı?
    [SerializeField] private string m_LockedText = "Door is Locked (Key Required)";
    [SerializeField] private float m_UnlockDuration = 2f; // Kilidi açmak için basılı tutma süresi
    [SerializeField] private ItemData m_RequiredKey; 

    private bool m_IsOpen = false;
    private Quaternion m_ClosedRotation;
    private Quaternion m_OpenRotation;

    // BASE CLASS OVERRIDES
    // IInteractable implementasyonu yerine "override" yazıyoruz.
    
    public override float InteractionDuration 
    {
        get 
        {
            // 1. Kapı kilitli mi?
            if (m_IsLocked)
            {
                // Anahtar varsa süre gönder, yoksa anında tepki ver
                return InventoryManager.Instance.HasDoorKey ? m_UnlockDuration : 0f;
            }
            
            // 2. Kilit açıldıktan sonra kapı anında (0 sn) açılsın
            return m_Duration; 
        }
    }

    // Interact fonksiyonunu override ediyoruz
    public override void Interact()
    {
        if (m_IsLocked)
        {
            if (InventoryManager.Instance.HasDoorKey)
            {
                Debug.Log("Unlocking the door...");
                m_IsLocked = false; // Kilidi aç
                ExecuteRotation(); // Ve kapıyı hareket ettir
            }
            else
            {
                Debug.Log("Locked! Cannot open.");
                // Burada bir 'tık tık' sesi çalabilirsin
            }
            return; // Kilitliyse aşağıya geçme
        }

        // 2. Kilidi açıksa normal aç/kapa yap
        ExecuteRotation();
    }

    // Text fonksiyonunu override ediyoruz
    public override string GetInteractionText()
    {
        if (m_IsLocked)
        {
            return InventoryManager.Instance.HasDoorKey ? "Press [E] to Unlock Door" : m_LockedText;
        }
        return m_IsOpen ? "Press [E] to Close Door" : "Press [E] to Open Door";
    }

    private void Start()
    {
        m_ClosedRotation = transform.rotation;
        m_OpenRotation = m_ClosedRotation * Quaternion.Euler(0, m_OpenAngle, 0);
    }

    private void ExecuteRotation()
    {
        StopAllCoroutines();
        m_IsOpen = !m_IsOpen;
        Quaternion target = m_IsOpen ? m_OpenRotation : m_ClosedRotation;
        StartCoroutine(RotateDoor(target));
        InventoryManager.Instance.RemoveItem(m_RequiredKey);
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
}