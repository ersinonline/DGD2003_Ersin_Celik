using UnityEngine;

// Ersin Çelik - Kilitli Kapı Sistemi (Hatalar Temizlendi)
public class KapiKontrol : MonoBehaviour
{
    [Header("Kapı Ayarları")]
    public int kapiNo = 1;
    public bool kilitli = true;
    public bool acik = false;

    [Header("Animasyon")]
    public Vector3 acikRotasyon = new Vector3(0, 90, 0);
    public Vector3 kapaliRotasyon = Vector3.zero;
    public float acilmaHizi = 2f;

    void Update()
    {
        // Kapıyı yumuşak bir şekilde hareket ettir
        Quaternion hedefRot = Quaternion.Euler(acik ? acikRotasyon : kapaliRotasyon);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, hedefRot, Time.deltaTime * acilmaHizi);
    }

    public void KapiyiAc()
    {
        // Kapı zaten açıksa kapat, kapalıysa aç (Toggle)
        kilitli = false; 
        acik = !acik;
        
        Debug.Log(acik ? "Sistem: Kapı açılıyor..." : "Sistem: Kapı kapanıyor...");
    }
}