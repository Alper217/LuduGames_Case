using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : InteractableBase
{
    [Header("Bed Settings")]
    [SerializeField] private float m_InteractionDuration = 2f; // Yatağa yatmak biraz zaman alsın (basılı tutma)
    [SerializeField] private string m_InteractionText = "Press and hold [E] to Sleep (Save Game)";

    [Header("Fade Settings")]
    [SerializeField] private Image m_FadeImage;
    [SerializeField] private float m_FadeDuration = 1.0f;

    // OVERRIDE EDİYORUZ: Kendi özel değerlerimizi gönderiyoruz.
    public override float InteractionDuration => m_InteractionDuration;
    public override string GetInteractionText() => m_InteractionText;
    
    // CanInteract yazmama gerek yok, zaten Base Class'ta "true" geliyor. Kod azaldı!
    
    public override void Interact()
    {
        Debug.Log("Interacting with Bed...");
        SaveGame();
    }

    private void SaveGame()
    {   
        Debug.Log("Game Saved!");
        // SaveGameData(); // İleride buraya kayıt kodları gelecek
        if (m_FadeImage != null)
        {
            StartCoroutine(FadeSequence());
        }
        else
        {
            Debug.LogWarning("Fade Image is assigned! Cannot fade screen.");
        }
    }

    private IEnumerator FadeSequence()
    {
        // 1. Ekranı Karart
        yield return StartCoroutine(Fade(1f, m_FadeDuration));
            
        // 2. Karanlıkta biraz bekle (Uyuyor hissi)
        yield return new WaitForSeconds(1f);
            
        // 3. Ekranı Aydınlat
        yield return StartCoroutine(Fade(0f, m_FadeDuration));
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
        
        // Emin olmak için son değeri ata
        Color finalColor = m_FadeImage.color;
        finalColor.a = targetAlpha;
        m_FadeImage.color = finalColor;
    }
}
