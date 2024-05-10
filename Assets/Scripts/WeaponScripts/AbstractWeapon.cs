using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    [SerializeField] private string weaponName;
    //[SerializeField] private float weaponDamage;
    [SerializeField] private float weaponRadius;
    [SerializeField] private float weaponFireRite;
    [SerializeField] private Bullet bulletPrefab;
    public string WeaponName
    {
        get { return weaponName; }
        set { weaponName = value; }
    }

    //public float WeaponDamage
    //{
    //    get { return weaponDamage; }
    //    set { weaponDamage = value; }
    //}

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

    public abstract void OnDrawGizmos();
    public abstract void ScanArea();
    //public abstract void bulletSpawn();
}
