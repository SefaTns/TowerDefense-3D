using UnityEngine;

public class PlugAndDrop : MonoBehaviour
{
    public GameObject objectPrefab; 
    public Collider areaCollider; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Mouse pozisyonundan bir ışın oluştur
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Işının collider alanıyla temas ettiğini kontrol et
            if (Physics.Raycast(ray, out hit) && hit.collider == areaCollider)
            {
                Instantiate(objectPrefab, hit.point, Quaternion.identity);
            }
        }
    }
}
