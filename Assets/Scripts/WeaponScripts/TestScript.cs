using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    //public GameObject mermi;
    //public Vector3 spawnPosition;
    //public Vector3 spawnRotation;

    ////private void Start()
    ////{
    ////    Quaternion rotation = Quaternion.Euler(spawnRotation);
    ////    Debug.Log(rotation.ToString());

    ////    Instantiate(mermi, spawnPosition, rotation);
    ////}

    //void Start()
    //{
    //    // Objenin instantiate edilmesi
    //    GameObject spawnedObject = Instantiate(mermi, spawnPosition, Quaternion.identity);

    //    // Transform üzerinden rotasyonun ayarlanmasý
    //    spawnedObject.transform.rotation = Quaternion.Euler(spawnRotation);
    //}

    public GameObject objectPrefab;
    public Transform target; // Hedef nesne (örneðin, düþman)
    public Vector3 spawnRotation;
    public Vector3 spawnPosition;

    void Start()
    {
        // Dinamik olarak rotasyon belirleme (örneðin, hedefe doðru dönme)
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

        // Objenin belirtilen pozisyon ve rotasyon ile instantiate edilmesi
        GameObject spawnedObject = Instantiate(objectPrefab, spawnPosition, rotationToTarget);
        spawnedObject.transform.rotation = Quaternion.Euler(spawnRotation);
        //spawnedObject.transform.rotation = Quaternion.LookRotation(directionToTarget);
    }


}
