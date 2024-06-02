using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopWeapon : AbstractWeapon
{
    private Collider[] enemies;
    private EnemyScript currentEnemy;
    [SerializeField] private Transform bulletNavig;

    private List<GameObject> enemiesList = new List<GameObject>();
    private void Start()
    {
        InvokeRepeating(nameof(ScanArea), 0, WeaponFireRite);
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, WeaponRadius);
    }

    public void ScanArea()
    {
        enemies = Physics.OverlapSphere(TowerTransform().position, WeaponRadius);
        Debug.Log("Enemies Length : " + enemies.Length);

        float distance = float.MaxValue;

        foreach (Collider enemy in enemies)
        {
            if (enemy.gameObject.TryGetComponent(out EnemyScript EnemyScript))
            {
                float dist = Vector3.Distance(bulletNavig.position, enemy.transform.position);
                if (dist <= distance)
                {
                    currentEnemy = EnemyScript;
                    distance = dist;
                }

            }
            else
                currentEnemy = null;
        }
        if (currentEnemy)
        {
            LoadArrow(bulletNavig, currentEnemy);
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        Debug.Log("Düþman alana girdi");
    //        enemiesList.Add(other.gameObject);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        Debug.Log("Düþman alandan çýktý");
    //        enemiesList.Remove(other.gameObject);
    //    }
    //}

    //private void ScanArea()
    //{
    //    float distance = float.MaxValue;

    //    foreach (GameObject enemy in enemiesList)
    //    {
    //        if (enemy.gameObject.CompareTag("Enemy"))
    //        {
    //            float dist = Vector3.Distance(bulletNavig.position, enemy.transform.position);
    //            if (dist <= distance)
    //            {
    //                currentEnemy = enemy;
    //                distance = dist;
    //            }
    //        }
    //    }

    //    if(currentEnemy && enemiesList.Count > 0)
    //        LoadArrow(bulletNavig, currentEnemy);
    //}

    private void Update()
    {
        if (currentEnemy)
        {
            Vector3 dir = currentEnemy.transform.position - transform.position;
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
