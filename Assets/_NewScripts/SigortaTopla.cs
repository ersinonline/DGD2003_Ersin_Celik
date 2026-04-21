using UnityEngine;
using UnityEngine.Events;

// Hazırlayan: Ersin Çelik
public class SigortaTopla : MonoBehaviour
{
    public static UnityEvent SigortaToplandiOlayi = new UnityEvent();

    public void Topla()
    {
        Debug.Log("Bilgi: Bir adet sigorta bulundu.");
        
        SigortaToplandiOlayi.Invoke();
        
        Destroy(gameObject);
    }
}
