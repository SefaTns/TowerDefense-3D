using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kule : MonoBehaviour
{
    private Collider[] enemies;
    [SerializeField] private Transform bulletNavig;
    [SerializeField] private float weaponRadius;
    [SerializeField] private float weaponFireRite;
    private GameObject currentEnemy;
    private void Start()
    {
        InvokeRepeating(nameof(ScanArea), 0, weaponFireRite);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, weaponRadius);
    }

    public void ScanArea()
    {
        enemies = Physics.OverlapSphere(transform.position, weaponRadius);

        float distance = float.MaxValue;

        foreach (Collider enemy in enemies)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                float dist = Vector3.Distance(bulletNavig.position, enemy.transform.position);
                if (dist <= distance)
                {
                    currentEnemy = enemy.gameObject;
                    distance = dist;
                }

            }
            else
            {
                currentEnemy = null;
            }

        }
    }

    private void Update()
    {
        if (currentEnemy)
        {
            Vector3 dir = (transform.position - currentEnemy.transform.position).normalized;
            Debug.Log(dir.ToString());
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
