using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, IInteractable
{
    [Header("Basic Settings")]
    [SerializeField] private float m_Speed = 2f;
    [SerializeField] private float m_OpenAngle = 90f;
    [SerializeField] private float m_Duration = 0f; // Açma süresi (basılı tutma)

    [Header("Lock Settings")]
    [SerializeField] private bool m_IsLocked = true; // Kapı kilitli başlasın mı?
    [SerializeField] private string m_LockedText = "Door is Locked (Key Required)";
    [SerializeField] private float m_UnlockDuration = 2f; // Kilidi açmak için basılı tutma süresi

    private bool m_IsOpen = false;
    private Quaternion m_ClosedRotation;
    private Quaternion m_OpenRotation;

    // Interface Gereksinimleri
    public float InteractionDuration 
{
    get 
    {
        // 1. Kapı kilitli mi?
        if (m_IsLocked)
        {
            // Anahtarımız varsa 2 saniye beklemesi için süre gönderiyoruz, 
            // Anahtar yoksa anında "Kilitli" uyarısı vermesi için 0 döndürüyoruz.
            return GameState.HasKey ? m_UnlockDuration : 0f;
        }
        
        // 2. Kilit açıldıktan sonra kapı anında (0 sn) açılsın
        return m_Duration; 
    }
}
    public bool CanInteract => true;
    public string InteractionText => GetInteractionText();

    private void Start()
    {
        m_ClosedRotation = transform.rotation;
        m_OpenRotation = m_ClosedRotation * Quaternion.Euler(0, m_OpenAngle, 0);
    }

    public void Interact()
    {
        if (m_IsLocked)
        {
            if (GameState.HasKey)
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

    public string GetInteractionText()
    {
        if (m_IsLocked)
        {
            return GameState.HasKey ? "Press [E] to Unlock Door" : m_LockedText;
        }
        return m_IsOpen ? "Press [E] to Close Door" : "Press [E] to Open Door";
    }
}