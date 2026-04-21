using UnityEngine;

// Ersin Çelik - Görev Yönetim Sistemi
public class GorevYoneticisi : MonoBehaviour
{
    [Header("Tamir Görevleri")]
    [Tooltip("Tamir edilecek bilgisayar objeleri.")]
    public BilgisayarTamir[] tamirEdilecekBilgisayarlar; // Değiştirildi: BilgisayarTamir
    
    private int aktifIndeks = 0;

    [Header("Yönlendirme")]
    public ArrowPointer yonOku; 

    void Start()
    {
        // Bilgisayarları karıştır
        ListeyiKaristir();

        foreach (var pc in tamirEdilecekBilgisayarlar)
        {
            pc.bozukMu = false; // Değiştirildi: bozukMu
            pc.TamirBittiOlayi.AddListener(SonrakiGorev); // Değiştirildi: TamirBittiOlayi
        }

        GoreviBaslat();
    }

    void ListeyiKaristir()
    {
        for (int i = 0; i < tamirEdilecekBilgisayarlar.Length; i++)
        {
            int rastgeleLokal = Random.Range(i, tamirEdilecekBilgisayarlar.Length);
            
            BilgisayarTamir gecici = tamirEdilecekBilgisayarlar[i];
            tamirEdilecekBilgisayarlar[i] = tamirEdilecekBilgisayarlar[rastgeleLokal];
            tamirEdilecekBilgisayarlar[rastgeleLokal] = gecici;
        }
    }

    void GoreviBaslat()
    {
        if (aktifIndeks < tamirEdilecekBilgisayarlar.Length)
        {
            // Sıradaki hedefi boz
            tamirEdilecekBilgisayarlar[aktifIndeks].bozukMu = true; // Değiştirildi: bozukMu
            
            if (yonOku != null)
            {
                yonOku.SetTarget(tamirEdilecekBilgisayarlar[aktifIndeks].transform);
            }
        }
        else
        {
            Debug.Log("Sistem: Tüm birimler başarıyla onarıldı!");
            if (yonOku != null) yonOku.gameObject.SetActive(false); 
        }
    }

    public void SonrakiGorev()
    {
        aktifIndeks++; 
        GoreviBaslat(); 
    }
}