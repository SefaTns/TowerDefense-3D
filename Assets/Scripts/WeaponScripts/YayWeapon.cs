using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YayWeapon : AbstractWeapon
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
        enemies = Physics.OverlapSphere(transform.position, WeaponRadius);

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
        }
        if (currentEnemy)
        {
            //Bullet bullet = Instantiate(WeaponBullet, transform.position, rotation);
            //bullet.SetTarget(currentEnemy.transform);
            LoadArrow(bulletNavig, currentEnemy);
        }
    }

    private void Update()
    {
        if (currentEnemy)
        {
            Vector3 dir = this.transform.position - currentEnemy.transform.position;
            dir.y = 0;

            this.transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}