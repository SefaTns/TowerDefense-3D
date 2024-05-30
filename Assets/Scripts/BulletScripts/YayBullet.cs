using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class YayBullet : Bullet
{
    void Start()
    {
        //yapmak istedigim sey bulletin x ini  dusmana bakar hale getirmek
        Debug.Log(transform.rotation);
        Debug.Log("Bullet rotation set");
    }

}