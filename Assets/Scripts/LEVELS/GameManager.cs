using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentLevelNumber;
    private int earnedStars;


    public static GameManager instance { get; private set; }
    public int totalCoin = 1000;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool SpendCoins(int amount)
    {
        if (totalCoin >= amount)
        {
            totalCoin -= amount;
            return true;
        }
        return false;
    }

    public void EarnCoin(int amount)
    {
        totalCoin += amount;
    }


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



        return 3;
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

    public void RestartGame()
    {
        Time.timeScale = 1f; // Oyunun hızını normale döndür
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Mevcut sahneyi yeniden yükler
    }

}
