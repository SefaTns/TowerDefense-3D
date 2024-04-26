using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SezonScript : MonoBehaviour
{
    public void SeviyeGecis()
    {
        SceneManager.LoadScene(2);
    }

    public void AnaMenuGecis()
    {
        SceneManager.LoadScene(0);
    }
}
