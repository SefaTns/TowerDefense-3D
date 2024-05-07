using UnityEngine;

public class PlugAndDrop: MonoBehaviour
{
    public GameObject objectPrefab; // Oluşturulacak nesnenin prefabı

    void Update()
    {
        //// Fare tıklamasını algıla
        //if (Input.GetMouseButtonDown(0)) // Sol tıklama
        //{
        //    // Fare tıklama konumunu al
        //    Vector3 clickPosition = Input.mousePosition;
        //    Debug.Log(Input.mousePosition);
        //    Debug.Log(clickPosition);
        //    //clickPosition.z = 10f;

        //    // Yeni nesneyi oluştur
        //    Instantiate(objectPrefab, clickPosition, Quaternion.identity);
        //    Debug.Log(Quaternion.identity);
        //}
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 clickPosition = hit.point;
                Instantiate(objectPrefab, clickPosition, Quaternion.identity);
            }
        }
    }
}
