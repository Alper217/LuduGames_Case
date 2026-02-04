# LLM Kullanım Dokümantasyonu

## Özet
- Toplam prompt sayısı: ~25 (yalnızca mimari ve kritik promptlar listelenmiştir)
- Kullanılan araçlar: Google Gemini (Antigravity Agent), ChatGPT-5.2, Web Gemini
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
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Projenin genişletilebilir olması için base class yapısı şarttı. Önerilen yapı Ludu Arts "Core System" gereksinimlerini doğrudan karşılıyordu, direkt uygulamadan sonra, kullanımı için bir adet entegresini "Door" scriptini AI'ye yaptırdım ve yapılış tarzına bakarak diğer kodları (Bed, Key, LightSwitch vs) entegre ettim. İşlem sonrası kontrolünü yaptırarak, kodun daha temiz ve anlaşılır olmasını sağladım.

---

## Prompt 2: Event Driven Interaction (Switch/Lever)

**Araç:** Google Gemini
**Tarih/Saat:** 2026-02-04 23:05

**Prompt:**
> "Chest" koduna "Door" kodu içerisinde erişmem lazım en sağlıklı nasıll erişirim? (Anahtar alınınca sandığın kilitlenmesi senaryosu)

**Alınan Cevap (Özet):**
> Doğrudan referans yerine `UnityEvent` kullanılması önerildi (Observer Pattern). `Key.cs` içine `OnKeyPickedUp` eventi eklendi ve `Chest.LockChest` metoduna bağlandı.

**Nasıl Kullandım:**
- [ ] Direkt kullandım
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
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Case gereksinimlerinde belirtilen "ScriptableObject ile item tanımları" maddesini karşılamak için bu yapı uygulandı. Kodu inceledikten sonra kendim denemek üzere yazmaya koyuldum lakin, zaten IDE'nin gelişmiş olması ve destekli olmasından kaynaklı olarak öngörüde kodun yazımında ekstra yardımı oldu, bu durumdan ötürü iki seçeneği de işaretlemiş bulunmaktayım.

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
- [ ] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Case'in en kritik değerlendirme kriteri olan standartlara uyum için kod tabanı baştan sona düzenlendi. Genelde yaptığım projelerde optimizasyon ve performans için gerekli olan yapıları (Singleton, ScriptableObject vs.) Yapay zekaya bırakıyorum ve yaptığını inceliyorum. Unity içerisinde yapabileceklerimi yapay zeka mentörlüğünden öğreniyorum ve uyguluyorum.

---

## Prompt 5: Refactoring Sonrası Hata Çözümü

**Araç:** Google Gemini
**Tarih/Saat:** 2026-02-04 21:00

**Prompt:**
> Refactoring sonrası `Door.cs` ve `Chest.cs` dosyalarında "ItemData could not be found" hatası alıyorum. Namespace eksikliği olabilir mi?

**Alınan Cevap (Özet):**
> Hatanın CS0246 koduyla eşleştiği ve `AlperKocasalih_Case_Project.Scripts` namespace'inin tanımlanmadığı tespit edildi. İlgili dosyaların en üstüne `using AlperKocasalih_Case_Project.Scripts;` eklenmesi veya namespace içine alınması önerildi.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Standartlara geçerken namespace düzenini oturttuktan sonra oluşan derleme hatalarını hızlıca çözmek için yapay zeka desteği aldım.

---

## Prompt 7: UI Feedback ve Progress Bar Mekaniği

**Araç:** Google Gemini
**Tarih/Saat:** 2026-02-05 01:20

**Prompt:**
> Süreli (Hold) etkileşim sırasında oyuncunun ne kadar süre daha basılı tutması gerektiğini anlaması için bir Progress Bar (dolum çubuğu) yapmak istiyorum. `m_HoldTimer` değerini UI Image FillAmount'a nasıl bağlarım?

**Alınan Cevap (Özet):**
> `m_HoldTimer / InteractionDuration` formülü ile 0-1 arasında bir oran elde edileceği ve bunun bir `Image.fillAmount` değerine atanması gerektiği belirtildi. UI güncellemelerinin `Update` yerine sadece etkileşim anında çalışması performans açısından önerildi.

**Nasıl Kullandım:**
- [ ] Direkt kullandım
- [x] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Kullanıcı deneyimini (UX) iyileştirmek adına mentorun önerdiği mantığı kullanarak dairesel bir dolum barı (Circular Progress Bar) tasarladım.

---

## Prompt 8: Unity Naming Conventions (Ludu Arts Standartları)

**Araç:** Google Gemini
**Tarih/Saat:** 2026-02-05 01:35

**Prompt:**
> Ludu Arts standartlarına göre field isimlendirmeleri nasıl olmalı? Özellikle serialize edilmiş private değişkenler için hangi prefix kullanılmalı?

**Alınan Cevap (Özet):**
> Private ve SerializeField değişkenler için `m_` prefix'i (member variable), PascalCase class isimleri ve interface'ler için `I` prefix'i kullanılması gerektiği vurgulandı. Kodun okunabilirliği için `#region` yapısının önemi açıklandı.

**Nasıl Kullandım:**
- [x] Direkt kullandım
- [ ] Adapte ettim
- [ ] Reddettim

**Açıklama:**
> Projedeki tüm değişken isimlendirmelerini bu standartlara göre refactor ettim. Bu, projenin profesyonel bir ekip tarafından devralınması durumunda sürdürülebilirliği (maintainability) sağlar.

---

## Genel Süreç Analizi ve Teknik Retrospektif

Prompt listesinin ötesinde, projenin 12 saatlik geliştirme sürecinde aşağıdaki teknik yaklaşımlar ve problem çözme stratejileri uygulanmıştır:

### 1. Mimari Karar: Interface vs. Inheritance
Başlangıçta sadece sınıflar üzerinden gidilmesi planlanırken, AI mentorluğu ile sistem **Interface-based** yapıya dönüştürülmüştür. 
- **Neden:** `IInteractable` kullanımı, `PlayerInteraction` scriptinin etkileşime girdiği nesnenin türünü bilmesine gerek kalmadan çalışmasını sağladı (Decoupling).

### 2. UI/UX Senkronizasyonu
Basılı tutma (Hold) mekaniğinde yaşanan "Geri Bildirim Eksikliği" sorunu, dinamik bir UI Progress Bar sistemiyle çözülmüştür.
- **Teknik Detay:** `m_HoldTimer / InteractionDuration` normalizasyonu kullanılarak, her karede (Update) UI Image'ın `fillAmount` değeri güncellenmiş ve kullanıcıya işlemin durumu anlık olarak yansıtılmıştır.

### 3. Ludu Arts Kod Standartları Uygulaması
Proje genelinde yapay zekanın önerdiği ham kodlar, şirketin `m_` prefix, `PascalCase` ve `#region` standartlarına göre manuel olarak refactor edilmiştir. Bu süreç, kodun okunabilirliğini ve ekip içi çalışmaya uygunluğunu (Maintainability) maksimize etmiştir.

### 4. Fizik ve Pivot Optimizasyonları
Kapıların açılma mekaniğindeki hatalı rotasyon sorunu, Pivot noktalarının "Empty Parent" yöntemiyle düzenlenmesiyle aşılmıştır. Bu, matematiksel rotasyon hesaplamalarının (`Quaternion.Slerp`) doğru eksende çalışmasını garantilemiştir.

## Sonuç (ÖNEMLİ)

Bu proje, yapay zekanın yalnızca bir "otomatik kod üretim aracı" olarak değil, teknik kararları doğrulayan ve mimari vizyon sunan bir "kıdemli danışman" rolünde konumlandırıldığı bir geliştirme sürecinin ürünüdür.

Süreç boyunca Antigravity (Gemini), ChatGPT-5.2 ve Web tabanlı Gemini modelleri hibrit bir yapıda kullanılmıştır. Her bir modelin güçlü yönlerinden faydalanılırken, özellikle Interface ve Inheritance gibi kritik mimari konseptlerin içselleştirilmesi hedeflenmiştir. Bu bağlamda:

1.  **Stratejik Öğrenme ve Doğrulama:** Konseptlerin sadece uygulanması değil, *neden* ve *nasıl* çalıştığının tam olarak anlaşılması için farklı AI modelleriyle izole sohbetler yürütülmüştür. Bu yöntem, tek bir modelin bağlam kirliliği yaşamasını engellemiş ve daha objektif teknik açıklamalar alınmasını sağlamıştır.
2.  **Seçici Dokümantasyon:** PROMPTS dosyasında, süreci boğacak yüzlerce küçük sözdizimi sorusu veya ince detay yerine; projenin mimarisini şekillendiren ve "Architectural Decision" niteliği taşıyan anahtar promptlara yer verilmiştir.
3.  **Bilinçli Refactoring ve Adaptasyon:** Yapay zekadan alınan ham kod blokları istisnalar dışında doğrudan projeye eklenmemiştir. Her öneri, Ludu Arts standartlarına ve Unity'nin best-practice yapılarına uygunluğu sorgulanarak manuel olarak refactor edilmiş, satır satır incelenmiş ve sindirilerek uygulanmıştır.

Özetle bu çalışma; AI destekli geliştirme sürecinin, "insan denetiminde, öğrenme odaklı ve mimari derinlikten ödün vermeden" nasıl verimli bir şekilde yönetilebileceğinin somut bir örneğidir. 