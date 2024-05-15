using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public GameObject objectToPlace; // Eklemek istediğimiz nesne
    private bool hasPlaced = false; // Nesne bir kere yerleştirildi mi?

    void Update()
    {
        // Nesne henüz yerleştirilmediyse ve fare sol tıklaması algılandıysa
        if (!hasPlaced && Input.GetMouseButtonDown(0))
        {
            // Fare pozisyonunu al ve bunu bir ışın olarak dönüştür
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Işın, bir nesneye çarptı mı kontrol et
            if (Physics.Raycast(ray, out hit))
            {
                // Çarpılan noktada yeni bir nesne oluştur
                GameObject newObject = Instantiate(objectToPlace, hit.point, Quaternion.identity);

                // Yeni nesneyi hedef noktanın merkezine yerleştir
                Vector3 offset = newObject.transform.position - newObject.GetComponent<Renderer>().bounds.center;
                newObject.transform.position = hit.point + offset;

                // Nesne yerleştirildiğinden, hasPlaced değerini true yap
                hasPlaced = true;
            }
        }
    }
}
