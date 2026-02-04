using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    float InteractionDuration { get; }
    bool CanInteract { get; }
    string InteractionText { get; }

    void Interact();
    string GetInteractionText();
}
