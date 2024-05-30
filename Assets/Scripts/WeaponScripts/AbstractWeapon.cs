using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    [SerializeField] private string weaponName;
    [SerializeField] private float weaponRadius;
    [SerializeField] private float weaponFireRite;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform towerTransform;
    public string WeaponName
    {
        get { return weaponName; }
        set { weaponName = value; }
    }

    public float WeaponRadius
    {
        get { return weaponRadius; }
        set { weaponRadius = value; }
    }

    public float WeaponFireRite
    {
        get { return weaponFireRite;}
        set { weaponFireRite = value;}
    }

    public Bullet WeaponBullet
    {
        get { return bulletPrefab; }
        set { bulletPrefab = value; }
    }

    public Transform TowerTransform()
    {
        return towerTransform;
    }
    public void LoadArrow(Transform konum, EnemyScript currentEnemy)
    {
        var enemy = FindObjectOfType<EnemyScript>();
        if (!enemy.IsDeath)
        {
            Bullet bullet = Instantiate(WeaponBullet, konum.position, Quaternion.identity);
            bullet.SetTarget(currentEnemy.transform);
        }
    }

    public abstract void OnDrawGizmos();
    //public abstract void ScanArea();
}
