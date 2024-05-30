using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    [SerializeField] private string bulletName;
    [SerializeField] private float speed;
    [SerializeField] private float damage;


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

    public void SetTarget(Transform target)
    {
        this.target = target;
    }


    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public string BulletName
    {
        get { return bulletName; }
        set { bulletName = value; }
    }
}