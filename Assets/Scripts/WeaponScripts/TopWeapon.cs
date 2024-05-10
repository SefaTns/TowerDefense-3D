using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopWeapon : AbstractWeapon
{
    private Collider[] enemies;
    private EnemyScript currentEnemy = null;
    private void Start()
    {
        InvokeRepeating(nameof(ScanArea), 0, WeaponFireRite);
    }

    public override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, WeaponRadius);
    }

    public override void ScanArea()
    {
        enemies = Physics.OverlapSphere(transform.position, WeaponRadius);

        float distance = 1000f;

        foreach (Collider enemy in enemies)
        {
            if (enemy.gameObject.TryGetComponent(out EnemyScript EnemyScript))
            {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if(dist <= distance) 
                {
                    currentEnemy = EnemyScript;
                    distance = dist;
                }
            }
        }
        if (currentEnemy)
        {
            Bullet bullet = Instantiate(WeaponBullet, transform.position, Quaternion.identity);
            bullet.SetTarget(currentEnemy.transform);
        }
    }

    //public override void bulletSpawn()
    //{
    //    throw new System.NotImplementedException();
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
