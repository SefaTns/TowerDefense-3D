using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.AdaptivePerformance.VisualScripting;

public class StoreScript : MonoBehaviour
{
    public TextMeshPro gold;
    public TextMeshPro ruby;
    public TextMeshPro clover;

    public void Start()
    {
       // gold.text = Other.Gameobject;
        ruby.text = PlayerPrefs.GetInt("Ruby").ToString();
        clover.text = PlayerPrefs.GetInt("Clover").ToString();
    }


    public void AnaMenuGecis()
    {
        SceneManager.LoadScene(0);
    }
}
