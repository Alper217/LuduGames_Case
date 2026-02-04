using UnityEngine;
using UnityEngine.Events;

public enum KeyType { ChestKey, DoorKey }

public class Key : InteractableBase
{
    [Header("Key Settings")]
    [SerializeField] private KeyType m_KeyType;
    public override string GetInteractionText() => m_InteractionText;
    [SerializeField] private string m_InteractionText = "Press and hold [E] to Pick Up Key";
    public UnityEvent OnKeyPickedUp;
    public override void Interact()
    {
        if (m_KeyType == KeyType.ChestKey)
        {
            GameState.ChestKey = true;
        }
        else if (m_KeyType == KeyType.DoorKey)
        {
            GameState.DoorKey = true;
        }
        Debug.Log("Key picked up!");
        OnKeyPickedUp.Invoke();
        Destroy(gameObject); // Anahtarı dünyadan sil
    }
}