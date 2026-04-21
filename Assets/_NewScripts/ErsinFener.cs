using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

// Hazırlayan: Ersin Çelik
// Hibrit Giriş Sistemli Fener
public class ErsinFener : MonoBehaviour
{
    public Light fenerIsigi; 
    private bool fenerAcik = false;

    void Start()
    {
        if (fenerIsigi == null) fenerIsigi = GetComponentInChildren<Light>();
        if (fenerIsigi != null) fenerIsigi.enabled = fenerAcik;
    }

    void Update()
    {
        bool lBasildi = Input.GetKeyDown(KeyCode.L);
        #if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null && Keyboard.current.lKey.wasPressedThisFrame) lBasildi = true;
        #endif

        if (lBasildi)
        {
            fenerAcik = !fenerAcik;
            if (fenerIsigi != null) fenerIsigi.enabled = fenerAcik;
        }
    }
}