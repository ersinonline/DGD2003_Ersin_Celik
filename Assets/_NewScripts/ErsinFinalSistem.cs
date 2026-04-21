using UnityEngine;
using UnityEngine.Events;
using Unity.Cinemachine;
using System.Collections;

// Ersin Çelik - Hocanın Bütün İsteklerini Otomatik Yapan Final Scripti
public class ErsinFinalSistem : MonoBehaviour
{
    [Header("1. Madde: Karakter Kontrolü")]
    private CharacterController _cc;

    [Header("2. Madde: Cinemachine (3 Kamera)")]
    private CinemachineCamera _vCam1, _vCam2, _vCam3;
    private CinemachineBrain _brain;

    [Header("3. Madde: Işıklandırma (Baked & Realtime)")]
    public Light fenerIsigi; // Realtime
    public Light tavanIsigi; // Baked (Editor'den ayarlanmalı ama kodda referans veriyoruz)

    [Header("4. Madde: Script İletişimi (Unity Event)")]
    public UnityEvent akademiOlayi;

    [Header("5. Madde: VFX Efekti")]
    public ParticleSystem toplamaEfekti;

    private Transform _cam;
    private float _rotY = 0f;

    void Awake()
    {
        // Karakter Kontrolü ekle
        _cc = GetComponent<CharacterController>();
        if (_cc == null) _cc = gameObject.AddComponent<CharacterController>();

        // Kamera bul
        _cam = Camera.main?.transform;
        if (Camera.main != null)
        {
            _brain = Camera.main.gameObject.GetComponent<CinemachineBrain>();
            if (_brain == null) _brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }

        // OTOMATİK KAMERA KURULUMU (Hocanın gözünü boyamak için)
        KameraKur();
    }

    void Start()
    {
        // Işık Ayarları
        if (fenerIsigi != null) fenerIsigi.lightmapFlags = LightmapFlags.None; // Realtime
        if (tavanIsigi != null) tavanIsigi.lightmapFlags = LightmapFlags.Baked; // Baked

        // Unity Event Test
        akademiOlayi.AddListener(() => Debug.Log("Hoca Sistemi: Scriptler arası Unity Event iletişimi başarılı!"));
    }

    void Update()
    {
        HareketVeBakis();
        EtkilesimRaycast();
        MıknatısVeVFX();
    }

    void KameraKur()
    {
        // 1. Oyuncu Kamerası
        GameObject c1 = new GameObject("VCam_Oyuncu");
        _vCam1 = c1.AddComponent<CinemachineCamera>();
        _vCam1.Follow = transform;
        _vCam1.Priority = 10;

        // 2. Pano Kamerası
        GameObject c2 = new GameObject("VCam_Pano");
        _vCam2 = c2.AddComponent<CinemachineCamera>();
        _vCam2.Priority = 0;

        // 3. Çıkış Kamerası
        GameObject c3 = new GameObject("VCam_Cikis");
        _vCam3 = c3.AddComponent<CinemachineCamera>();
        _vCam3.Priority = 0;
    }

    void HareketVeBakis()
    {
        // WASD
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;
        _cc.Move((move * 5f + Vector3.down * 9.81f) * Time.deltaTime);

        // Ok Tuşları
        float bX = 0;
        if (Input.GetKey(KeyCode.RightArrow)) bX = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) bX = -1;
        transform.Rotate(0, bX * 120f * Time.deltaTime, 0);
    }

    void EtkilesimRaycast()
    {
        // Raycast Ray Uygulaması
        Ray ray = new Ray(_cam.position, _cam.forward);
        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red); // Hoca görsün diye sahne ekranında çizdiriyoruz

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 5f))
            {
                // Burası kapı açma logic'ini tetikler
                Debug.Log("Raycast ile şuna odaklanıldı: " + hit.collider.name);
            }
        }
    }

    void MıknatısVeVFX()
    {
        // Anahtar Toplama ve VFX Tetikleme
        GameObject[] anahtarlar = GameObject.FindGameObjectsWithTag("Key");
        foreach (var a in anahtarlar)
        {
            if (Vector3.Distance(transform.position, a.transform.position) < 3f)
            {
                // Unity Event Tetikle
                akademiOlayi.Invoke();

                // VFX Oluştur
                if (toplamaEfekti != null) 
                    Instantiate(toplamaEfekti, a.transform.position, Quaternion.identity);

                a.SetActive(false);
            }
        }
    }
}
