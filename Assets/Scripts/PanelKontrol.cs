using UnityEngine;
using UnityEngine.UI;

public class PanelKontrol : MonoBehaviour
{
    public GameObject[] towerPrefabs; // Kule prefabrikleri
    private ClickHandler clickHandler;

    void Start()
    {
        // ClickHandler bileşenini bul
        clickHandler = FindObjectOfType<ClickHandler>();
        gameObject.SetActive(false); // Paneli başlangıçta gizle
    }

    public void ShowPanel(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true); // Paneli göster
    }

    public void SelectTower(int index)
    {
        clickHandler.SetSelectedTower(towerPrefabs[index]);
        gameObject.SetActive(false); // Paneli gizle
    }
}