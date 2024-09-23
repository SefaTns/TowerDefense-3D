using System;
using UnityEngine;
using UnityEngine.EventSystems;

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
                            selectionPanel.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("Dizide boş bir eleman yok.");
                        }
                    }
                    //}
                    //Debug.Log("defence point değil");
                    //if (selectionPanel.activeSelf && !IsPointerOverUIObject() && !DidHitPanel())
                    //{
                    //    Debug.Log("dışarıda");
                    //    selectionPanel.SetActive(false); // Paneli kapat
                    //    return; // Diğer işlemleri yapmadan çık
                    //}
                }
            }
        }
    }

    //private bool IsPointerOverUIObject()
    //{
    //    // Eğer UI elemanları üzerinde isek, true dönecek
    //    Debug.Log(EventSystem.current.IsPointerOverGameObject());
    //    return EventSystem.current.IsPointerOverGameObject();
    //}

    //private bool DidHitPanel()
    //{
    //    // Panelin pozisyonuna yakın olup olmadığını raycast ile kontrol et
    //    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    // Eğer ray panelin collider'ına çarparsa panel üzerinde tıklanmıştır
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        if (hit.collider.gameObject == selectionPanel)
    //        {
    //            Debug.Log("panele tıklanmış");
    //            return true; // Panel üzerine tıklanmış
    //        }
    //    }
    //    Debug.Log("Panel dışı");
    //    return false; // Panel dışına tıklanmış
    //}

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

    //public PaneliKapat()
    //{
    //    selectionPanel.SetActive(false);
    //}
}