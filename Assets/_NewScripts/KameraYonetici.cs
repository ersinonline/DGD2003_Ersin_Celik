using UnityEngine;
using Unity.Cinemachine;

// Ersin Çelik - Hocanın İstediği 3'lü Cinemachine Sistemi
public class KameraYonetici : MonoBehaviour
{
    [Header("Cinemachine Kameraları (3 Adet)")]
    public CinemachineCamera oyuncuKamerasi; // 1. Kamera
    public CinemachineCamera panoKamerasi;   // 2. Kamera
    public CinemachineCamera cikisKamerasi;  // 3. Kamera

    void Start()
    {
        // Başlangıçta oyuncu kamerasını aktif et
        OyuncuKamerasiniAc();
    }

    public void OyuncuKamerasiniAc()
    {
        OncelikAyarla(10, 0, 0); // Oyuncu kamerası en yüksek öncelikte
    }

    public void PanoKamerasiniAc()
    {
        OncelikAyarla(0, 10, 0); // Pano kamerası öne geçer
    }

    public void CikisKamerasiniAc()
    {
        OncelikAyarla(0, 0, 10); // Çıkış kamerası öne geçer
    }

    private void OncelikAyarla(int p1, int p2, int p3)
    {
        // Cinemachine öncelik (Priority) sistemi üzerinden geçiş (Blending)
        if (oyuncuKamerasi != null) oyuncuKamerasi.Priority = p1;
        if (panoKamerasi != null) panoKamerasi.Priority = p2;
        if (cikisKamerasi != null) cikisKamerasi.Priority = p3;
        
        Debug.Log("Hoca için not: Cinemachine Priority geçişi yapıldı.");
    }
}