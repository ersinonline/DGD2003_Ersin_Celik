using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

// Hazırlayan: Ersin Çelik
// Hibrit Giriş Sistemli Etkileşim
public class ErsinEtkilesim : MonoBehaviour
{
    public float etkilesimMesafesi = 5f; 
    public Transform oyuncuKamerasi;
    private string ekranMesaji = "";

    void Start()
    {
        if (oyuncuKamerasi == null) oyuncuKamerasi = Camera.main?.transform;
    }

    void Update()
    {
        if (oyuncuKamerasi == null) return;
        
        // Yazıyı her kare güncelle
        Ray isin = new Ray(oyuncuKamerasi.position, oyuncuKamerasi.forward);
        RaycastHit temas;
        if (Physics.Raycast(isin, out temas, etkilesimMesafesi))
        {
            if (temas.collider.CompareTag("Key") || temas.collider.CompareTag("Door"))
                ekranMesaji = "[E] Bas ve Al/Ac";
            else ekranMesaji = "";
        }
        else ekranMesaji = "";

        // Giriş Kontrolü
        bool eBasildi = Input.GetKeyDown(KeyCode.E);
        #if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame) eBasildi = true;
        #endif

        if (eBasildi && Physics.Raycast(isin, out temas, etkilesimMesafesi))
        {
            if (temas.collider.CompareTag("Key"))
            {
                var k = temas.collider.GetComponent<AnahtarTopla>();
                if (k != null) k.Topla();
            }
            else if (temas.collider.CompareTag("Door"))
            {
                var d = temas.collider.GetComponent<KapiKontrol>();
                if (d != null) d.KapiyiAc();
            }
        }
    }

    void OnGUI()
    {
        if (!string.IsNullOrEmpty(ekranMesaji))
        {
            GUI.Label(new Rect(Screen.width/2 - 50, Screen.height/2 + 50, 200, 30), ekranMesaji);
        }
    }
}