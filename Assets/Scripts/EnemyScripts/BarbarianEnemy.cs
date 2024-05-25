using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarbarianEnemy : EnemyScript
{
    

    

    public void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Door"))
        //{
        //    //transform.DOMove(MapManager.instance.tower.position, 1).OnComplete(() =>
        //    //{
        //    //    Destroy(this.gameObject);
        //    //});

        //}

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
