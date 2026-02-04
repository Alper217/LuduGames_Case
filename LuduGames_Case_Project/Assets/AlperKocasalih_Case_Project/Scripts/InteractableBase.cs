using UnityEngine;

public abstract class InteractableBase : MonoBehaviour, IInteractable
{
    [Header("Base Settings")]
    [SerializeField] protected string m_BaseInteractionText = "Interact";

    // Sanal (Virtual) property'ler: Alt sınıflar bunları ezebilir (override).
    public virtual float InteractionDuration => 0f;
    public virtual bool CanInteract => true;

    // Interface'deki InteractionText property'si
    // Default olarak GetInteractionText()'ten dönen değeri verir.
    public string InteractionText => GetInteractionText();

    // Soyut (Abstract) metot: Her alt sınıf kendi etkileşimini YAZMAK ZORUNDA.
    public abstract void Interact();

    // Sanal metot: İstenirse text dinamik olarak değiştirilebilir (Örn: Kapı Aç/Kapa)
    public virtual string GetInteractionText()
    {
        return m_BaseInteractionText;
    }
}
