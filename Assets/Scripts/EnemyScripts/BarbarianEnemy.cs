using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarbarianEnemy : EnemyScript
{
    private Animator anim;
    private NavMeshAgent agent;
    private float wait = 2f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(AnimWait());
        agent.SetDestination(MapManager.instance.tower.position);

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("leftDoor") || other.gameObject.CompareTag("rightDoor"))
        {
            agent.isStopped = true;
            anim.SetBool("attackBool", true);
        }
        var doorComp = other.gameObject.GetComponent<DoorTrigger>();
        if (doorComp.DoorHealt == 0)
        {
            agent.isStopped = false;
            anim.SetBool("attackBool", false);
            anim.SetBool("walkBool", true);
        }
    }
    private IEnumerator AnimWait()
    {
        agent.isStopped = true;
        anim.SetBool("walkBool", false);
        yield return new WaitForSeconds(wait);
        agent.isStopped = false;
        anim.SetBool("walkBool", true);
    }



    public void OnTriggerEnter(Collider other)
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


            if (this.HealtMove - a.Damage > 0)
            {
                this.HealtMove -= a.Damage;
                Debug.Log(this.HealtMove);
            }
            else
            {
                Destroy(this.gameObject);
            }
            Destroy(other.gameObject);


        }
    }

}
