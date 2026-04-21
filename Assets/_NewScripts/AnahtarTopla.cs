using UnityEngine;

// Ersin Çelik - Akıllı Anahtar (Trigger'ı kendi ayarlar)
public class AnahtarTopla : MonoBehaviour
{
    [Header("Anahtar Kimliği")]
    public int anahtarNo = 1;

    void Awake()
    {
        // OTOMATİK AYAR: Ersin tek tek uğraşmasın diye Trigger'ı biz açalım
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            // Eğer Mesh Collider ise Convex olması gerekir, yoksa Trigger çalışmaz
            if (col is MeshCollider meshCol)
            {
                meshCol.convex = true;
            }
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player etiketli bir şey dokunduysa
        if (other.CompareTag("Player"))
        {
            Topla();
        }
    }

    public void Topla()
    {
        ErsinSihirbaz es = Object.FindFirstObjectByType<ErsinSihirbaz>();
        if (es != null)
        {
            if (!es.toplananAnahtarlar.Contains(anahtarNo))
            {
                es.toplananAnahtarlar.Add(anahtarNo);
                Debug.Log("Sistem: " + anahtarNo + " nolu anahtar otomatik toplandı.");
            }
        }
        
        Destroy(gameObject); 
    }
}