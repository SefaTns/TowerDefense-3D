using System;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    public Vector3[] positions;  // Adres tutan dizi (örneğin, Transform pozisyonları)
    public GameObject kulePanel; // Kule seçim paneli
    private GameObject selectedTower; // Seçilen kule
    private Vector3 hitPosition; // Tıklanan pozisyon

    void Start()
    {
        // Kule panelini başlangıçta gizle
        kulePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Collider clickedCollider = hit.collider;
                if (clickedCollider.CompareTag("Defence Point"))
                {
                    bool isEmptySpot = CheckForEmptySpot();
                    bool isPositionDuplicated = CheckForDuplicatePosition(hit.collider.transform.position);

                    if (isPositionDuplicated)
                    {
                        Debug.Log("Yeni pozisyon daha önce girilmiş.");
                        return;
                    }

                    if (isEmptySpot)
                    {
                        hitPosition = hit.collider.transform.position;
                        kulePanel.GetComponent<UIHandler>().ShowPanel(Input.mousePosition);
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
        PlaceTower();
    }

    private void PlaceTower()
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

    bool CheckForEmptySpot()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (positions[i] == Vector3.zero)
            {
                return true;
            }
        }
        return false;
    }

    bool CheckForDuplicatePosition(Vector3 newPosition)
    {
        foreach (Vector3 position in positions)
        {
            if (position == newPosition)
            {
                Debug.Log(newPosition);
                return true;
            }
        }
        return false;
    }
}
