using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public GameObject[] towerPrefabs; // Kule prefabrikleri
    public GameObject currentSelectedTower;
    private ClickHandler clickHandler;

    void Start()
    {
        // ClickHandler bileşenini bul
        clickHandler = FindObjectOfType<ClickHandler>();

        // İlk seçilen kuleyi varsayılan olarak ayarla
        currentSelectedTower = towerPrefabs[0];
    }

    public void SelectTower(int index)
    {
        currentSelectedTower = towerPrefabs[index];
        clickHandler.SetSelectedTower(currentSelectedTower);
        gameObject.SetActive(false); // Paneli gizle
    }

    public void ShowPanel(Vector3 position)
    {
        transform.position = position; // Paneli tıklama pozisyonuna taşı
        gameObject.SetActive(true); // Paneli göster
    }
}
