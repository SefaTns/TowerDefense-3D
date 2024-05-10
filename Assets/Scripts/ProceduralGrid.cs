using UnityEngine;

public class ProceduralGrid : MonoBehaviour
{
    public int gridSizeX = 5; // Grid'in X boyutu
    public int gridSizeY = 5; // Grid'in Y boyutu
    public float cellSize = 1f; // Her bir hücrenin boyutu
    public GameObject cellPrefab; // Hücrenin prefabı

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Grid'in başlangıç konumu
        Vector3 startPos = transform.position - new Vector3(gridSizeX / 2f * cellSize, 0f, gridSizeY / 2f * cellSize);

        // Grid'in oluşturulması
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                // Her bir hücrenin konumu
                Vector3 cellPos = startPos + new Vector3(x * cellSize, 0f, y * cellSize);

                // Hücrenin oluşturulması ve yerleştirilmesi
                GameObject cell = Instantiate(cellPrefab, cellPos, Quaternion.identity);
                cell.transform.parent = transform; // Grid'in altında olmasını sağla

                // Hücreye bir collider ekleyerek sınırları belirle
                BoxCollider collider = cell.AddComponent<BoxCollider>();
                collider.size = new Vector3(cellSize, 0.1f, cellSize); // Collider boyutu hücre boyutuna uygun olmalı
            }
        }
    }
}
