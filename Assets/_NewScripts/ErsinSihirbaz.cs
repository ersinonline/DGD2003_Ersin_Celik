using UnityEngine;
using System.Collections.Generic;

// Ersin Çelik - İsim Dedektifi Mıknatıs Sistemi
public class ErsinSihirbaz : MonoBehaviour
{
    public float hiz = 5f;
    public float bakisHassasiyeti = 120f;
    public List<int> toplananAnahtarlar = new List<int>();

    private CharacterController _cc;
    private Transform _cam;
    private bool _fenerAcik = false;

    void Start()
    {
        _cc = GetComponentInParent<CharacterController>();
        if (_cc == null) _cc = gameObject.AddComponent<CharacterController>();
        _cam = Camera.main?.transform;
        toplananAnahtarlar.Clear();
    }

    void Update()
    {
        MıknatısGuncelle();
        HareketVeBakis();
        
        if (Input.GetKeyDown(KeyCode.L)) {
            _fenerAcik = !_fenerAcik;
            Light f = Object.FindFirstObjectByType<Light>();
            if (f != null) f.enabled = _fenerAcik;
        }

        if (Input.GetKeyDown(KeyCode.E) && _cam != null)
        {
            Ray ray = new Ray(_cam.position, _cam.forward);
            if (Physics.SphereCast(ray, 0.7f, out RaycastHit hit, 7f))
            {
                // Kapı ve Pano isminden tanıyalım (Tag derdi bitsin)
                string name = hit.collider.name.ToLower();
                if (name.Contains("door") || name.Contains("kapi")) {
                    var k = hit.collider.GetComponent<KapiKontrol>();
                    if (k != null && toplananAnahtarlar.Contains(k.kapiNo)) k.KapiyiAc();
                }
                else if (name.Contains("panel") || name.Contains("pano")) {
                    var p = hit.collider.GetComponent<ElektrikPanosu>();
                    if (p != null && (toplananAnahtarlar.Contains(1) || toplananAnahtarlar.Contains(2))) p.Etkilesim();
                }
            }
        }
    }

    void MıknatısGuncelle()
    {
        // Tag (Etiket) aramayı bırakıp İSİM aramaya başlıyoruz (En garantisi)
        GameObject[] tumObjeler = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        
        foreach (GameObject obj in tumObjeler)
        {
            if (obj == null) continue;
            string objIsmi = obj.name.ToLower();

            // İsminde "key" veya "anahtar" geçiyorsa
            if (objIsmi.Contains("key") || objIsmi.Contains("anahtar"))
            {
                float mesafe = Vector3.Distance(transform.position, obj.transform.position);
                
                if (mesafe < 10f) // Menzili 10 metreye çıkardım
                {
                    int no = objIsmi.Contains("2") ? 2 : 1;
                    if (!toplananAnahtarlar.Contains(no))
                    {
                        toplananAnahtarlar.Add(no);
                        Debug.Log("Dedektif Yakaladı: " + obj.name + " (Mesafe: " + mesafe + ")");
                    }
                    obj.SetActive(false); // Objeyi çantaya at
                }
            }
        }
    }

    void HareketVeBakis()
    {
        float bX = 0; if (Input.GetKey(KeyCode.RightArrow)) bX = 1; if (Input.GetKey(KeyCode.LeftArrow)) bX = -1;
        transform.parent.Rotate(0, bX * bakisHassasiyeti * Time.deltaTime, 0);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 move = (transform.parent.right * h + transform.parent.forward * v).normalized;
        _cc.Move((move * hiz + Vector3.down * 9.81f) * Time.deltaTime);
    }

    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 260, 100), "ERSİN DEDEKTİF MODU");
        GUI.Label(new Rect(20, 40, 240, 30), "Çantadakiler: " + string.Join(", ", toplananAnahtarlar));
        GUI.Label(new Rect(20, 70, 240, 30), "Durum: Anahtarlar Aranıyor...");
    }
}
