using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [SerializeField] private string bulletName;
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if (this.target)
        {
            Vector3 dir = target.position - transform.position;
            transform.forward = dir;
            transform.Translate(transform.forward * Time.deltaTime * speed, Space.World);
        }
        else
            Destroy(gameObject);
    }

    public float getDamage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    public string BulletName
    {
        get
        {
            return bulletName;
        }
        set
        {
            bulletName = value;
        }

    }
}
