using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentLevelNumber;
    private int earnedStars;

    // Seviye tamamland���nda bu metodu �a��r�n
    public void OnLevelCompleted()
    {
        // Burada earnedStars de�eri seviye performans�na g�re hesaplanmal�d�r
        earnedStars = CalculateStars(); // �rne�in, 1 ile 3 aras�nda bir de�er d�nd�ren bir metod

        // GameDataManager'dan CompleteLevel metodunu �a��r�n
        FindObjectOfType<GameDataManager>().CompleteLevel(currentLevelNumber, earnedStars);
    }

    // Y�ld�zlar� hesaplayan �rnek bir metod (kendi oyun mekaniklerinize g�re uyarlay�n)
    private int CalculateStars()
    {
        // Bu metod, oyuncunun performans�na g�re y�ld�z say�s�n� hesaplamal�d�r
        // �rne�in, zaman, puan veya di�er kriterlere dayal� olabilir
        return 3; // Basit bir �rnek olarak 3 y�ld�z d�nd�r�yoruz
    }

    // �rnek olarak seviyeyi tamamlamay� tetiklemek i�in bir tu�a basma kontrol� ekleyin
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // C tu�una bas�ld���nda seviye tamamlanm�� say�lacak
        {
            Debug.Log("Seviye Tamamland�.");
            OnLevelCompleted();
        }
    }
}
