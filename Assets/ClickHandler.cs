using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public GameObject objectToPlace; // Eklemek istediğimiz nesne
    private bool hasPlaced = false; // Nesne bir kere yerleştirildi mi?


    void OnTriggerEnter(Collider other)
    {
        // Tetikleyici alan içine bir nesne girdiğinde bu fonksiyon çağrılır
        // other parametresi, tetikleyici alan içine giren nesneyi temsil eder

        // Tetikleyici alan içine giren nesnenin tag'ini kontrol et
        if (other.CompareTag("tower"))
        {
            // Tetikleyici alan içine giren nesne, tower tag'ine sahip bir nesne ile temas etti
            Debug.Log("Tıklanan nokta, tower tag'ine sahip bir nesne ile temas etti!");
            // İstenen işlemleri yapabilirsiniz
        }
    }
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
                // Tıklanan nesnenin tag'ini kontrol et
                if (hit.collider.CompareTag("Defence Point"))
                {
                    Debug.Log(objectToPlace.tag);
                    // Tıklanan nesnenin merkezine yeni bir nesne oluştur
                    GameObject newObject = Instantiate(objectToPlace, hit.collider.transform.position, Quaternion.identity);

                    // Yeni nesneyi hedef noktanın merkezine yerleştir
                    Vector3 offset = newObject.transform.position - newObject.GetComponent<Renderer>().bounds.center;
                    newObject.transform.position = hit.collider.transform.position + offset;

                    // Nesne yerleştirildiğinden, hasPlaced değerini true yap
                    hasPlaced = true;
                }
            }
        }
        hasPlaced = false;
    }
}

