using UnityEngine;

public class ZoomWithFieldOfView : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomSpeed = 0.1f; // Daha düşük bir hız, mobil cihazlar için daha uygundur
    public float minFov = 15f;
    public float maxFov = 90f;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            // İki parmakla dokunma kontrolü
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Her iki dokunmanın önceki pozisyonları
            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            // Önceki ve şimdiki dokunma pozisyonları arasındaki mesafeyi hesapla
            float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;

            // Mesafe farkını hesapla (pozitifse yakınlaştırma, negatifse uzaklaştırma)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // Field of View değerini ayarla
            float fov = mainCamera.fieldOfView;
            fov += deltaMagnitudeDiff * zoomSpeed;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            mainCamera.fieldOfView = fov;
        }
    }
}
