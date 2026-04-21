using UnityEngine;
using UnityEngine.Events;

// Ersin Çelik - Hocanın İstediği Unity Event Sistemi
public class AnahtarTopla : MonoBehaviour
{
    public int anahtarNo = 1;

    // HOCANIN İSTEDİĞİ KRİTER: Scriptler arası Unity Event iletişimi
    [Header("Script İletişim Olayı")]
    public UnityEvent anahtarToplandiOlayi;

    void Awake()
    {
        // Otomatik Trigger ayarı
        Collider col = GetComponent<Collider>();
        if (col != null) {
            if (col is MeshCollider meshCol) meshCol.convex = true;
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Topla();
        }
    }

    public void Topla()
    {
        // Olayı tetikle (Diğer scriptler bunu duyacak)
        if (anahtarToplandiOlayi != null) anahtarToplandiOlayi.Invoke();
        
        // Envantere ekle (ErsinSihirbaz'a haber ver)
        ErsinSihirbaz es = Object.FindFirstObjectByType<ErsinSihirbaz>();
        if (es != null) es.toplananAnahtarlar.Add(anahtarNo);

        Debug.Log("Hoca için not: Unity Event tetiklendi ve anahtar toplandı.");
        Destroy(gameObject);
    }
}