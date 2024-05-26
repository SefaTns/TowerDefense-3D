using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEditor;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] private float healthMove;
    [SerializeField] private float damage;
    [SerializeField] private float magicResistance;
    [SerializeField] private float armor;

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Door"))
    //    {
    //        transform.DOMove(MapManager.instance.tower.position, 1).OnComplete(() =>
    //        {
    //            Destroy(this.gameObject);
    //        });
    //    }

    //    if (other.gameObject.CompareTag("Bullet"))
    //    {
    //        var a = other.gameObject.GetComponent<Bullet>();
    //        Debug.Log(a.BulletName);


    //        if (this.healthMove - a.Damage > 0)
    //        {
    //            this.healthMove -= a.Damage;
    //            Debug.Log(this.healthMove);
    //        }
    //        else
    //        {
    //            Destroy(this.gameObject);
    //        }
    //        Destroy(other.gameObject);


    //    }
    //}

    public float HealtMove
    {
        get { return healthMove; }
        set { healthMove = value; }
    }

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public float MagicResistance
    {
        get { return magicResistance; }
        set { magicResistance = value; }
    }

    public float Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    public void damageControl(float damage, float armor, float magicResistance)
    {
        float arm = armor - this.Armor;
        float mag = magicResistance - this.MagicResistance;

        if (armor > this.Armor)
            this.HealtMove -= damage + (damage * arm) / 100;
        else
        {
            if (damage + arm < this.Armor)
            {
                float yuzde = (damage * arm) / 100;
                this.HealtMove -= yuzde;
            }
            else
                this.HealtMove -= damage - arm;
        }

        if (magicResistance > this.MagicResistance)
            this.HealtMove -= damage + (damage * mag) / 100;
        else
        {
            if (damage + mag < this.MagicResistance)
            {
                float yuzde = (damage * mag) / 100;
                this.HealtMove -= yuzde;
            }
            else
                this.HealtMove -= damage - mag;
        }
    }
}
