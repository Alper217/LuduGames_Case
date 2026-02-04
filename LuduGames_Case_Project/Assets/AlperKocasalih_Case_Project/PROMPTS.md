# LLM Kullanım Dokümantasyonu

## Özet
- Toplam prompt sayısı: 27
- Kullanılan araçlar: Google Gemini (Antigravity Agent)
- En çok yardım alınan konular:
    - Unity Event System entegrasyonu (Observer Pattern)
    - Envanter sistemi mantığı (ScriptableObject yapısı)
    - Kod refactoring (Ludu Arts standartlarına uyum)

---

## Prompt 1: Kod Mimarisi ve Base Class

**Araç:** Google Gemini
**Tarih/Saat:** 2026-02-04 22:30

**Prompt:**
> Refactoring Interaction System to use a base class (InteractableBase) and interfaces (IInteractable) for better code organization.

**Alınan Cevap (Özet):**
> `InteractableBase` abstract sınıfı ve `IInteractable` interface'i oluşturuldu. Kod tekrarını önlemek için ortak özellikler base class'a taşındı.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Projenin genişletilebilir olması için base class yapısı şarttı. Önerilen yapı Ludu Arts "Core System" gereksinimlerini doğrudan karşılıyor.

---

## Prompt 2: Event Driven Interaction (Switch/Lever)

**Araç:** Google Gemini
**Tarih/Saat:** 2026-02-04 23:05

**Prompt:**
> Chest koduna doorda erişmem lazım en sağlıklı naısl erişirim? (Anahtar alınınca sandığın kilitlenmesi senaryosu)

**Alınan Cevap (Özet):**
> Doğrudan referans yerine `UnityEvent` kullanılması önerildi (Observer Pattern). `Key.cs` içine `OnKeyPickedUp` eventi eklendi ve `Chest.LockChest` metoduna bağlandı.

**Nasıl Kullandım:**
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Spagetti kodu önlemek ve modülerliği korumak için UnityEvent sistemi en temiz çözümdü.

---

## Prompt 3: Inventory System Mantığı

**Araç:** Google Gemini
**Tarih/Saat:** 2026-02-04 23:38

**Prompt:**
> Basic, 3 slotluk envanteri nasıl yapabiliriz?

**Alınan Cevap (Özet):**
> `ScriptableObject` tabanlı `ItemData` ve Singleton `InventoryManager` yapısı kuruldu. `Key.cs` ve `Door.cs` bu sisteme entegre edilerek `ItemType` kontrolü eklendi.

**Nasıl Kullandım:**
- [x] Direkt kullandım

**Açıklama:**
> Case gereksinimlerinde belirtilen "ScriptableObject ile item tanımları" maddesini karşılamak için bu yapı uygulandı.

---

## Prompt 4: Refactoring to Standards

**Araç:** Google Gemini
**Tarih/Saat:** 2026-02-05 00:30

**Prompt:**
> Bu projeyi Ludu Arts standartlarına (coding conventions, naming, regions, XML docs) göre refactor et.

**Alınan Cevap (Özet):**
> Tüm scriptlere `m_` prefix'i, `#region` blokları ve XML dokümantasyonu eklendi. Namespace `AlperKocasalih_Case_Project.Scripts` olarak standartlaştırıldı.

**Nasıl Kullandım:**
- [x] Direkt kullandım

**Açıklama:**
> Case'in en kritik değerlendirme kriteri olan standartlara uyum için kod tabanı baştan sona düzenlendi.
