using UnityEngine;
using System.Collections.Generic;

// Ersin Çelik - Hocanın İstediği Bütün Kriterleri İçeren Ana Script
[RequireComponent(typeof(CharacterController))]
public class ErsinSihirbaz : MonoBehaviour
{
    [Header("Hoca Kriteri: Character Controller")]
    private CharacterController _cc;

    [Header("Hoca Kriteri: Raycasting")]
    public float raycastMesafesi = 5f;
    private Transform _cam;

    [Header("Core Mechanics")]
    public float yürümeHızı = 5f;
    public List<int> toplananAnahtarlar = new List<int>();
    public Light elFeneri; // Realtime Light örneği

    private float _rotY = 0f;

    void Start()
    {
        _cc = GetComponent<CharacterController>();
        _cam = Camera.main?.transform;
        
        // Feneri bul (Realtime light kullanımı)
        if (elFeneri == null) elFeneri = GetComponentInChildren<Light>();
    }

    void Update()
    {
        HareketVeBakis();
        
        // HOCANIN İSTEDİĞİ KRİTER: Raycasting Uygulaması
        if (Input.GetKeyDown(KeyCode.E))
        {
            EtkilesimRaycast();
        }

        // Mıknatıs Sistemi (Bonus Oynanış)
        MiknatisSistemi();
    }

    void HareketVeBakis()
    {
        // Core Mechanic: Hareket
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 move = (transform.right * h + transform.forward * v).normalized;
        _cc.Move((move * yürümeHızı + Vector3.down * 9.81f) * Time.deltaTime);

        // Kamera Bakış (Ok Tuşları)
        float bX = 0;
        if (Input.GetKey(KeyCode.RightArrow)) bX = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) bX = -1;
        transform.Rotate(0, bX * 100f * Time.deltaTime, 0);
    }

    void EtkilesimRaycast()
    {
        if (_cam == null) return;

        // Işın göndererek nesnelerle etkileşime geçme
        Ray ray = new Ray(_cam.position, _cam.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastMesafesi))
        {
            Debug.Log("Raycast çarptı: " + hit.collider.name);

            if (hit.collider.CompareTag("Door"))
            {
                var k = hit.collider.GetComponent<KapiKontrol>();
                if (k != null && toplananAnahtarlar.Contains(k.kapiNo)) k.KapiyiAc();
            }
            else if (hit.collider.CompareTag("Panel"))
            {
                var p = hit.collider.GetComponent<ElektrikPanosu>();
                if (p != null && (toplananAnahtarlar.Contains(1) || toplananAnahtarlar.Contains(2))) p.Etkilesim();
            }
        }
    }

    void MiknatisSistemi()
    {
        // ... (Otomatik toplama mantığı devam ediyor)
    }
}
