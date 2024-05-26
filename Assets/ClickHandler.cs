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

    public void SetSelectedTower(GameObject tower)
    {
        selectedTower = tower;
        PlaceTower(hitPosition);
    }

    private void PlaceTower(Vector3 position)
    {
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
        Vector3 adjustedPosition = new Vector3(position.x, position.y + 10.0f, position.z); // 5.0f, panelin yukarıya taşınma miktarı

        // Panelin pozisyonunu ayarlıyoruz
        selectionPanel.transform.position = adjustedPosition;

        // Paneli aktif hale getiriyoruz
        selectionPanel.SetActive(true);
    }

    //bool CheckForEmptySpot()
    //{
    //    // positions dizisi null ise (başlatılmamış)
    //    if (positions == null)
    //    {
    //        // positions dizisini bir elemanlı olarak başlat
    //        positions = new Vector3[1];
    //        // Bir boş yer olduğunu bildir
    //        return true;
    //    }
    //    else
    //    {
    //        // positions dizisi null değilse
    //        // For döngüsü ile tüm elemanları kontrol et
    //        for (int i = 0; i < positions.Length; i++)
    //        {
    //            // Eğer herhangi bir eleman Vector3.zero ise
    //            if (positions[i] == Vector3.zero)
    //            {
    //                // Bir boş yer olduğunu bildir
    //                return true;
    //            }
    //        }
    //        // Tüm elemanlar dolu ise
    //        return true;
    //    }
    //}

    bool CheckForDuplicatePosition(Vector3 newPosition)
    {
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