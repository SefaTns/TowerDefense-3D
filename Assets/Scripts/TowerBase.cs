using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerBase : MonoBehaviour
{
    public string TowerName;
    public int Cost;
    public float Range;
    public float FireRate;
    public GameObject BulletPrefab;
    private float FireCoolDown = 0f;

    //ates etme kodlari buraya tasinacak

    //void Update()
    //{
    //    fireCooldown -= Time.deltaTime;

    //    if (fireCooldown <= 0f)
    //    {
    //        FireAtTarget();
    //        fireCooldown = 1f / fireRate;
    //    }
    //}

    //void FireAtTarget()
    //{
    //    // Hedefi bulma ve ateş etme kodları
    //    // Bu örnek basit bir ateş etme işlemi içeriyor
    //    Debug.Log(towerName + " is firing!");
    //}
}
