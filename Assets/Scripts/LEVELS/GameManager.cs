using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentLevelNumber;
    private int earnedStars;

    // Seviye tamamlandýðýnda bu metodu çaðýrýn
    public void OnLevelCompleted()
    {
        // Burada earnedStars deðeri seviye performansýna göre hesaplanmalýdýr
        earnedStars = CalculateStars(); // Örneðin, 1 ile 3 arasýnda bir deðer döndüren bir metod

        // GameDataManager'dan CompleteLevel metodunu çaðýrýn
        FindObjectOfType<GameDataManager>().CompleteLevel(currentLevelNumber, earnedStars);
    }

    // Yýldýzlarý hesaplayan örnek bir metod (kendi oyun mekaniklerinize göre uyarlayýn)
    private int CalculateStars()
    {
        // Bu metod, oyuncunun performansýna göre yýldýz sayýsýný hesaplamalýdýr
        // Örneðin, zaman, puan veya diðer kriterlere dayalý olabilir
        return 3; // Basit bir örnek olarak 3 yýldýz döndürüyoruz
    }

    // Örnek olarak seviyeyi tamamlamayý tetiklemek için bir tuþa basma kontrolü ekleyin
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // C tuþuna basýldýðýnda seviye tamamlanmýþ sayýlacak
        {
            Debug.Log("Seviye Tamamlandý.");
            OnLevelCompleted();
        }
    }

    public void RestartLevel()
    {
        //// Aktif sahneyi yeniden yükleyerek seviyeyi tekrar başlat
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex));
        
    }

}
