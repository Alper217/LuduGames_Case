using UnityEngine;
using UnityEngine.Events;

// KeyType'ı silebiliriz çünkü artık ItemData içinde ItemType var. 
// Ama eski kodlar hata vermesin diye şimdilik tutabilirim veya silebiliriz.
// Şimdilik siliyorum, çünkü ItemData kullanacağız.

public class Key : InteractableBase
{
    [Header("Key Settings")]
    [SerializeField] private ItemData m_ItemData; // Artık ScriptableObject referansı alıyoruz!
    [SerializeField] private string m_InteractionText = "Press [E] to Pick Up Key";
    public UnityEvent OnKeyPickedUp;

    public override string GetInteractionText() => m_InteractionText;

    public override void Interact()
    {
        if (m_ItemData != null)
        {
            if(InventoryManager.Instance.AddItem(m_ItemData))
            {
                Debug.Log($"Key picked up: {m_ItemData.itemName}");
                OnKeyPickedUp.Invoke();
                Destroy(gameObject); // Anahtarı dünyadan sil
            }
            else
            {
                Debug.Log("Inventory is full!");
            }
        }
        else
        {
            Debug.LogError("Key scriptinde ItemData atanmamış!");
        }
    }
}