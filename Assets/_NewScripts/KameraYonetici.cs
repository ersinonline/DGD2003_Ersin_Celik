using UnityEngine;

// Ersin Çelik - Kamera Geçiş Sistemi (Hatasız Versiyon)
// Bu versiyonda paket hatası almamak için kütüphaneleri sildim
public class KameraYonetici : MonoBehaviour
{
    [Header("Kameralar (Objeleri buraya sürükle)")]
    public GameObject oyuncuKameraObjesi;
    public GameObject panoKameraObjesi;
    public GameObject cikisKameraObjesi;

    [Header("Referanslar")]
    public MonoBehaviour oyuncuKontrolu;

    void Start()
    {
        // Otomatik bulma
        if (oyuncuKontrolu == null) oyuncuKontrolu = Object.FindFirstObjectByType<ErsinKarakterKontrol>();
        OyuncuKamerasiniAc();
    }

    public void PanoKamerasiniAc()
    {
        KameralariKapat();
        if (panoKameraObjesi != null) panoKameraObjesi.SetActive(true);
        if (oyuncuKontrolu != null) oyuncuKontrolu.enabled = false;
    }

    public void CikisKamerasiniAc()
    {
        KameralariKapat();
        if (cikisKameraObjesi != null) cikisKameraObjesi.SetActive(true);
        if (oyuncuKontrolu != null) oyuncuKontrolu.enabled = false;
    }

    public void OyuncuKamerasiniAc()
    {
        KameralariKapat();
        if (oyuncuKameraObjesi != null) oyuncuKameraObjesi.SetActive(true);
        if (oyuncuKontrolu != null) oyuncuKontrolu.enabled = true;
    }

    private void KameralariKapat()
    {
        if (oyuncuKameraObjesi != null) oyuncuKameraObjesi.SetActive(false);
        if (panoKameraObjesi != null) panoKameraObjesi.SetActive(false);
        if (cikisKameraObjesi != null) cikisKameraObjesi.SetActive(false);
    }
}