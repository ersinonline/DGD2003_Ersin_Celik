# Sıra Bizde - Oyun Tasarım Belgesi (GDD)

**Hazırlayan:** Ersin Çelik  
**Proje Kodu:** DGD2003  
**Tür:** Korku / Kaçış / Gizem  

## 1. Oyun Özeti
"Sıra Bizde", gece vakti okulda kilitli kalan bir öğrencinin (Ersin) okulun karanlık dehlizlerinden geçerek elektrik panosuna ulaşma ve okuldan kaçma hikayesini konu alır. Oyuncu, sınırlı pil ömrüne sahip feneriyle karanlığı aydınlatmalı, kilitli kapıları doğru anahtarlarla açmalı ve finalde elektrik panosunu aktif ederek okulu aydınlatmalıdır.

## 2. Temel Mekanikler
*   **Gelişmiş Hareket Sistemi:** WASD ile hareket ve Ok Tuşları ile bağımsız kamera bakışı.
*   **Hibrit Giriş Yönetimi:** Oyun, hem Unity'nin yeni "Input System" paketini hem de klasik "Input Manager" sistemini destekler (Teknik Özgünlük).
*   **Akıllı Envanter (Magnet System):** Oyuncu, anahtarların yakınına geldiğinde (10 metre) anahtarlar otomatik olarak envantere eklenir. Bu, akıcı bir oynanış (Gameplay Flow) sağlamak için özel olarak kodlanmıştır.
*   **Senaryo Bazlı Etkileşim:** Kapılar ve Elektrik Panosu, ilgili anahtarlar olmadan açılamaz.
*   **Dinamik Aydınlatma:** Pille çalışan el feneri (L tuşu) ve oyun sonunda aktif olan okul ışıkları.

## 3. Oyun Akışı (Scenario Flow)
1.  **Başlangıç:** Oyuncu boş koridorda başlar.
2.  **İlk Engel:** Kilitli Sınıf Kapısı.
3.  **Görev 1:** Koridorda gizlenmiş olan "1 Nolu Anahtarı" bul ve sınıfı aç.
4.  **Görev 2:** Sınıfın içindeki masada duran "Pano Anahtarını" (2 Nolu) al.
5.  **Final:** Koridorun sonundaki Elektrik Panosuna git, panoyu aç ve okulun elektriğini geri getir.

## 4. Teknik Detaylar
*   **Dil:** C# (Tamamen Türkçe isimlendirmeler ve yorum satırları ile geliştirildi).
*   **Oyun Motoru:** Unity 6 (6000.3.2f1).
*   **Assetler:** Özel seçilmiş 3D modeller ve optimize edilmiş URP (Universal Render Pipeline) aydınlatma.

---
*Bu proje, DGD2003 dersi kapsamında Ersin Çelik tarafından özgün bir senaryo ve mekaniklerle hazırlanmıştır.*
