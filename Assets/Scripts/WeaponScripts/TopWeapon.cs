using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopWeapon : AbstractWeapon
{
    private Collider[] enemies;
    private EnemyScript currentEnemy = null;
    [SerializeField] private Transform bulletNavig;
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

        float distance = float.MaxValue;

        foreach (Collider enemy in enemies)
        {
            if (enemy.gameObject.TryGetComponent(out EnemyScript EnemyScript))
            {
                float dist = Vector3.Distance(bulletNavig.position, enemy.transform.position);
                if(dist <= distance) 
                {
                    currentEnemy = EnemyScript;
                    distance = dist;
                }
            }
        } 
        if (currentEnemy)
        {
            LoadArrow(bulletNavig, currentEnemy);
        }

    }

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
