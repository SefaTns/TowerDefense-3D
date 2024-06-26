using UnityEngine;

public class ZoomWithFieldOfView : MonoBehaviour
{
    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel"); // Fare tekerleği girişini al
        if (scroll != 0f)
        {
            Debug.Log("Mouse ScrollWheel değeri: " + scroll);
        }
    }
}
