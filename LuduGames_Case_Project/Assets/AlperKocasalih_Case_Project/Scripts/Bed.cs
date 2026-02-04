using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour, IInteractable
{
    [SerializeField] private float m_InteractionDuration = 0f;
    [SerializeField] private bool m_CanInteract = true;
    [SerializeField] private string m_InteractionText = "Press and hold [E] to Sleep (Save your game)";

    [Header("Fade Settings")]
    [SerializeField] private Image m_FadeImage;
    [SerializeField] private float m_DefaultDuration = 1.0f;

    public float InteractionDuration { get => m_InteractionDuration; }
    public bool CanInteract { get => m_CanInteract; }
    public string InteractionText { get => m_InteractionText; }
    public void Interact()
    {
        SaveGame();
    }
    public void SaveGame()
    {   
        Debug.Log("Game Saved");
        //SaveGameData()
        StartCoroutine(FadeSequence());

    }

    private void SaveGameData()
    {
        //Can Add Save game data here
    }

    private IEnumerator FadeSequence()
    {
        // 1. Kararma (Fade Out)
        yield return StartCoroutine(Fade(1f, m_DefaultDuration));
            
        // 2. Tam karanlıkta bekleme (Opsiyonel)
        yield return new WaitForSeconds(0.5f);
            
        // 3. Aydınlanma (Fade In)
        yield return StartCoroutine(Fade(0f, m_DefaultDuration));
    }

    private IEnumerator Fade(float targetAlpha, float duration)
    {
        float startAlpha = m_FadeImage.color.a;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
                
            Color color = m_FadeImage.color;
            color.a = newAlpha;
            m_FadeImage.color = color;
                
            yield return null;
        }
    }

    public string GetInteractionText() { return InteractionText; }
}
