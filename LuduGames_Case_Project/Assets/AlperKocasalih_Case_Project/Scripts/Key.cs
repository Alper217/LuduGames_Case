using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public float InteractionDuration => 0f;
    public bool CanInteract => true;
    public string InteractionText => m_InteractionText;
    [SerializeField] private string m_InteractionText = "Press and hold [E] to Pick Up Key";
    public void Interact()
    {
        GameState.HasKey = true; // Artık anahtarımız var!
        Debug.Log("Key picked up!");
        Destroy(gameObject); // Anahtarı dünyadan sil
    }

    public string GetInteractionText() { return m_InteractionText; }
}