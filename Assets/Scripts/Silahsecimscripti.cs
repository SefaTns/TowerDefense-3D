using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silahsecimscripti : MonoBehaviour
{
    public GameObject silahSecim;
    public void gorunurluk()
    {
        silahSecim.SetActive(!silahSecim.activeSelf);
    }

}
