# Interaction System - Alper Kocasalih

## Kurulum
- **Unity Versiyonu:** 2022.3.61f1 (Veya projenin açıldığı stabil sürüm)
- **Kurulum:**
  1. Projeyi Unity Hub üzerinden açın.
  2. `Assets/AlperKocasalih_Case_Project/Scenes/TestScene` sahnesini açın.
  3. Play tuşuna basın.

## Nasıl Test Edilir
- **Kontroller:**
  - `W, A, S, D`: Hareket
  - `Mouse`: Kamera kontrolü
  - `E`: Etkileşim (Bas veya Basılı Tut)
  - `ESC`: Çıkış (Editörde durdurur)

- **Test Senaryoları:**
  1. **Kapı (Door):** Kilitli kapıya gidin. "Locked" uyarısını görün.
  2. **Anahtar (Key):** Yerdeki anahtarı `E` ile alın. Envantere eklenir.
  3. **Sandık (Chest):**
     - Anahtarsız deneyin (Açılmaz).
     - Etraftaki ilgili anahtarı bulun.
     - Sandığa gelip `E` tuşuna basın. Kapının anahtarını sandıktan alın ve kapıya yönelin.
  4. **Işık Anahtarı (Switch):** Duvardaki anahtara basın, ışıkların açılıp kapandığını gözlemleyin.
  5. **Kapı (Door):** Kilitli kapıya gidin, `E`tuşuna basın ve kilitli kapıyı açın.
  6. **Yatak (Bed):** Yatağa gidin ve `E` tuşuna basılı tutun. Progress bar dolunca ekran kararır (Sleep).

## Mimari Kararlar
Bu projede **OOP (Nesne Yönelimli Programlama)** prensipleri ve **Observer Pattern** kullanılmıştır.

1. **InteractableBase & IInteractable:**
   - Tüm etkileşimli nesneler (Door, Chest, Key) `InteractableBase` sınıfından türetilmiştir.
   - Bu sayede kod tekrarı önlenmiş ve `PlayerInteraction` scriptinin sadece `IInteractable` arayüzünü bilmesi yeterli olmuştur (Dependency Inversion).

2. **Inventory System (ScriptableObject):**
   - Eşyalar (Item) birer `ScriptableObject` olarak tasarlanmıştır (`ItemData`).
   - Bu sayede yüzlerce farklı eşya kod yazmadan, sadece Asset oluşturarak eklenebilir.
   - `InventoryManager` singleton yapısıyla merkezi bir veri deposu sağlar.

3. **Event Driven Design:**
   - Nesneler arası (Örn: Anahtar -> Sandık) doğrudan bağlantı yerine `UnityEvent` kullanılmıştır.
   - Bu sayede bir anahtar alındığında ne olacağı (Sandık mı açılacak, Işık mı yanacak) tamamen Editör üzerinden, kod değiştirmeden ayarlanabilir.

## Ludu Arts Standartlarına Uyum
Proje geliştirilirken Ludu Arts coding convention dokümanlarına sadık kalınmıştır:

- **Naming Convention:**
  - Private field'lar `m_` prefix ile isimlendirildi (Örn: `m_IsOpen`).
  - Static field'lar `s_` prefix aldı.
  - Public property'ler PascalCase yapıldı.
  
- **Documentation:**
  - Tüm public metodlar ve sınıflar `///` XML summary blokları ile dokümante edildi.
  
- **Code Structure:**
  - `#region` blokları kullanılarak Fields, Properties, Methods ayrıştırıldı.
  - Namespace (`AlperKocasalih_Case_Project.Scripts`) kullanılarak kod izole edildi.

## Bilinen Limitasyonlar
- **Animation:** Animator yerine kod tabanlı (Coroutine/Tween) basit animasyonlar tercih edildi. (Zaman yönetimi açısından)
- **UI:** Envanter UI'ı şu an için Unity'nin basit ikonlarından oluşmaktadır, sürükle-bırak veya detay penceresi yoktur. Öncelik olarak basit bir UI tasarımı yapıldı.

## Ekstra Özellikler (Bonus)
- **SphereCast Detection:** Oyuncunun etkileşim konforunu artırmak için Raycast yerine SphereCast kullanıldı.
- **Dynamic Interaction Text:** Nesnenin durumuna göre (Örn: "Locked" vs "Open") değişen dinamik metinler.
- **UnityEvent Integration:** Kolay genişletilebilirlik için event sistemi.
