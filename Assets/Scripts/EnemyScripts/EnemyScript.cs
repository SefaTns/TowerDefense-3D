using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEditor;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float healthMove;
    private NavMeshAgent agent;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.SetDestination(MapManager.instance.tower.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            transform.DOMove(MapManager.instance.tower.position, 1).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            var a = other.gameObject.GetComponent<Bullet>();
            Debug.Log(a.BulletName);
            

            if (this.healthMove - a.Damage > 0)
            {
                this.healthMove -= a.Damage;
                Debug.Log(this.healthMove);
            }
            else
            {
                Destroy(this.gameObject);
            }
            Destroy(other.gameObject);

            
        }
    }
}
