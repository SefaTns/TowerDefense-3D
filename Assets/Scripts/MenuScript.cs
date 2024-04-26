using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    public void SezonGecis()
    {
        SceneManager.LoadScene(1);
    }
    public void StoreGecis()
    {
        SceneManager.LoadScene(3);
    }

}
