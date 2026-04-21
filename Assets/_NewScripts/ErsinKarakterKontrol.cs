using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

// Hazırlayan: Ersin Çelik
// Hem ESKİ hem YENİ Giriş Sistemlerini Destekleyen Hibrit Kontrolcü
public class ErsinKarakterKontrol : MonoBehaviour
{
    public float yürümeHızı = 5f;
    public float zıplamaGücü = 5f;
    public float yerçekimi = -12f;
    public Transform oyuncuKamerası;

    private CharacterController karakterKontrolcü;
    private float dikeyHız = 0f;
    private float kameraDikeyAçı = 0f;

    void Start()
    {
        karakterKontrolcü = GetComponent<CharacterController>();
        if (oyuncuKamerası == null) oyuncuKamerası = Camera.main?.transform;
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Time.timeScale == 0f && !Input.GetKeyDown(KeyCode.P)) return;

        // P - DURAKLATMA (Hibrit)
        bool pBasildi = Input.GetKeyDown(KeyCode.P);
        #if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame) pBasildi = true;
        #endif
        if (pBasildi) Time.timeScale = (Time.timeScale == 0f) ? 1f : 0f;

        if (Time.timeScale == 0f) return;

        BakışVeHareket();
    }

    void BakışVeHareket()
    {
        // --- BAKIŞ (Ok Tuşları) ---
        float bX = 0, bY = 0;
        if (Input.GetKey(KeyCode.RightArrow)) bX = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) bX = -1;
        if (Input.GetKey(KeyCode.UpArrow)) bY = 1;
        if (Input.GetKey(KeyCode.DownArrow)) bY = -1;
        #if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null) {
            if (Keyboard.current.rightArrowKey.isPressed) bX = 1;
            if (Keyboard.current.leftArrowKey.isPressed) bX = -1;
            if (Keyboard.current.upArrowKey.isPressed) bY = 1;
            if (Keyboard.current.downArrowKey.isPressed) bY = -1;
        }
        #endif

        if (oyuncuKamerası != null) {
            kameraDikeyAçı -= bY * 100f * Time.deltaTime;
            kameraDikeyAçı = Mathf.Clamp(kameraDikeyAçı, -60f, 60f);
            oyuncuKamerası.localEulerAngles = Vector3.right * kameraDikeyAçı;
            transform.Rotate(Vector3.up * bX * 100f * Time.deltaTime);
        }

        // --- HAREKET (WASD) ---
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        #if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null) {
            if (Keyboard.current.wKey.isPressed) v = 1;
            if (Keyboard.current.sKey.isPressed) v = -1;
            if (Keyboard.current.aKey.isPressed) h = -1;
            if (Keyboard.current.dKey.isPressed) h = 1;
        }
        #endif

        Vector3 hareket = (transform.right * h + transform.forward * v).normalized;
        if (karakterKontrolcü.isGrounded) {
            dikeyHız = -1f;
            bool zıpla = Input.GetKeyDown(KeyCode.Space);
            #if ENABLE_INPUT_SYSTEM
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) zıpla = true;
            #endif
            if (zıpla) dikeyHız = zıplamaGücü;
        }
        dikeyHız += yerçekimi * Time.deltaTime;
        karakterKontrolcü.Move((hareket * yürümeHızı + Vector3.up * dikeyHız) * Time.deltaTime);
    }
}