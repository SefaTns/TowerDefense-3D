using System;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public Vector3[] positions;  // Adres tutan dizi (örneğin, Transform pozisyonları)
    public GameObject selectionPanel; // Seçim paneli
    private GameObject selectedTower; // Seçilen kule
    private Vector3 hitPosition; // Tıklanan pozisyonun merkezi
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        // Seçim panelini başlangıçta gizle
        selectionPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Collider clickedCollider = hit.collider;
                if (clickedCollider.CompareTag("Defence Point"))
                {
                    //eğer tıkladığımız noktada daha önceden panel aktifse tekrar tıklandığı için paneli kapat 
                    if (selectionPanel.activeSelf)
                    {
                        selectionPanel.SetActive(false);
                    }
                    else //eğer tıklanılan yer yerleştirme noktası değilse yani hangi kuleyi yerleştireceğimizi
                    //seçtiysek ve tıkladığımız yerde kule yoksa yeni kule yerleştirme işlemini yap   
                    {
                        bool isPositionDuplicated = CheckForDuplicatePosition(hit.collider.transform.position);
                        hitPosition = hit.transform.position;
                        Debug.Log("duplicated: " + isPositionDuplicated);
                        if (isPositionDuplicated)
                        {
                            Debug.Log("Yeni pozisyon daha önce girilmiş.");
                            return;
                        }

                        if (!isPositionDuplicated)
                        {
                            Debug.Log("Yeni pozisyon girildi.");
                            ShowSelectionPanel(hitPosition); // Paneli göster
                        }
                        else
                        {
                            Debug.Log("Dizide boş bir eleman yok.");
                        }
                    }
                }
            }
        }
    }

    public void SetSelectedTower(GameObject tower)
    {
        Debug.Log("Seçilen kule: " + tower.name);
        selectedTower = tower;
        PlaceTower(hitPosition);
    }

    private void PlaceTower(Vector3 position)
    {
        Debug.Log("Kule yerleştiriliyor: " + position);
        if (selectedTower != null)
        {
            GameObject newObject = Instantiate(selectedTower, hitPosition, Quaternion.identity);
            int index = Array.FindIndex(positions, item => item == Vector3.zero);

            if (index == -1)
            {
                Vector3[] newArray = new Vector3[positions.Length + 1];
                positions.CopyTo(newArray, 0);
                positions = newArray;
                index = positions.Length - 1;
            }

            positions[index] = newObject.transform.position;
            Debug.Log("Yeni pozisyon eklendi: " + positions[index]);
        }
    }

    private void ShowSelectionPanel(Vector3 position)
    {
        // Panelin y pozisyonunu biraz yukarı taşıyoruz
        Vector3 adjustedPosition = new Vector3(position.x, position.y + 2.0f, position.z); // 5.0f, panelin yukarıya taşınma miktarı

        // Panelin pozisyonunu ayarlıyoruz
        selectionPanel.transform.position = adjustedPosition;

        // Paneli aktif hale getiriyoruz
        selectionPanel.SetActive(true);
    }

    bool CheckForDuplicatePosition(Vector3 newPosition)
    {
        Debug.Log("Yeni pozisyon: " + newPosition);
        foreach (Vector3 position in positions)
        {
            if (position == newPosition)
            {
                Debug.Log(newPosition + "pozisyon duplice edilmiş");
                return true;
            }
        }
        return false;
    }
}