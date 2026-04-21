using UnityEngine;

// Ersin Çelik - Elektrik Panosu (İsim hatası düzeltildi)
public class ElektrikPanosu : MonoBehaviour
{
    [Header("Gereken Anahtar")]
    public int gerekenAnahtarNo = 2; // Pano için gereken anahtar numarası

    [Header("Okul Işıkları")]
    public GameObject[] okulIsiklari; 
    
    private bool panoAcildi = false;

    public void Etkilesim()
    {
        if (panoAcildi) return;
        
        Debug.Log("Sistem: Elektrik Panosu açıldı, şalter indirildi.");
        IsiklariYak();
        panoAcildi = true;
    }

    void IsiklariYak()
    {
        foreach (GameObject isik in okulIsiklari)
        {
            if (isik != null) isik.SetActive(true);
        }
        Debug.Log("OKULUN ELEKTRİĞİ GELDİ! TEBRİKLER ERSİN!");
    }
}