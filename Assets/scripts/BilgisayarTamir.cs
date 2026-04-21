using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Ersin Çelik - Bilgisayar Tamir Mekaniği
public class BilgisayarTamir : MonoBehaviour
{
    [Header("Arayüz Elemanları")]
    [Tooltip("Tamir uyarısı yazısı")]
    public GameObject tamirYazisi; 
    
    [Tooltip("İlerleme çubuğu çerçevesi")]
    public GameObject dolumCubuguPanel; 
    
    [Tooltip("Dolacak olan bar görseli")]
    public Image barGorseli; 

    [Header("Tamir Ayarları")]
    public float tamirSuresi = 3f; 
    private float mevcutIlerleme = 0f;

    [Header("Durum")]
    public bool bozukMu = false; 
    
    public UnityEvent TamirBittiOlayi;

    private bool oyuncuYakininda = false; 

    void Start()
    {
        if (tamirYazisi != null) tamirYazisi.SetActive(false);
        if (dolumCubuguPanel != null) dolumCubuguPanel.SetActive(false);
        if (barGorseli != null) barGorseli.fillAmount = 0f;
    }

    void Update()
    {
        if (bozukMu && oyuncuYakininda)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (tamirYazisi != null) tamirYazisi.SetActive(false);
                if (dolumCubuguPanel != null) dolumCubuguPanel.SetActive(true);

                mevcutIlerleme += Time.deltaTime;
                
                if (barGorseli != null) 
                {
                    barGorseli.fillAmount = mevcutIlerleme / tamirSuresi;
                }

                if (mevcutIlerleme >= tamirSuresi)
                {
                    TamiriTamamla();
                }
            }
            else 
            {
                mevcutIlerleme = 0f;
                if (barGorseli != null) barGorseli.fillAmount = 0f;
                
                if (dolumCubuguPanel != null) dolumCubuguPanel.SetActive(false);
                if (tamirYazisi != null) tamirYazisi.SetActive(true);
            }
        }
    }

    void TamiriTamamla()
    {
        bozukMu = false;
        mevcutIlerleme = 0f;
        
        if (tamirYazisi != null) tamirYazisi.SetActive(false);
        if (dolumCubuguPanel != null) dolumCubuguPanel.SetActive(false);
        
        Debug.Log("Sistem: " + gameObject.name + " onarıldı.");
        
        TamirBittiOlayi.Invoke(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && bozukMu)
        {
            oyuncuYakininda = true;
            if (tamirYazisi != null) tamirYazisi.SetActive(true); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            oyuncuYakininda = false;
            mevcutIlerleme = 0f;
            
            if (tamirYazisi != null) tamirYazisi.SetActive(false); 
            if (dolumCubuguPanel != null) dolumCubuguPanel.SetActive(false);
            if (barGorseli != null) barGorseli.fillAmount = 0f;
        }
    }
}