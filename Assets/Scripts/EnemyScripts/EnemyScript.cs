﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] public float healthMove;
    [SerializeField] private float damage;
    [SerializeField] private float magicResistance;
    [SerializeField] private float armor;
    private bool isDeath = false;

    private Animator anim;
    private NavMeshAgent agent;
    private float wait = 1f;

    [SerializeField] private LayerMask hitlayer;
    [SerializeField] private Transform body;
    private RaycastHit hit;

    private float nextAttackTime = 0f;

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
    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (!this.IsDeath)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 0.22f;
            //Debug.DrawRay(transform.position, forward, Color.red, 1.0f);

            if (Physics.Raycast(body.position, forward, out hit, 0.22f, hitlayer))
            {
                DoorTrigger doorComp = hit.transform.GetComponent<DoorTrigger>();
                if (hit.collider.CompareTag("ShieldDoor") && doorComp.CanTrigger)
                {
                    if(doorComp.DoorHealt > 0)
                    {
                        AnimAttack();
                        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                        float animationDuration = stateInfo.length;
                        nextAttackTime = Time.time + animationDuration;

                        doorComp.DoorHealt -= this.Damage;
                    }
                    else
                    {
                        doorComp.CanTrigger = false;
                        AnimWalk();
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
            else
            {
                AnimWalk();
            }

        }
    }

    private void AnimWalk()
    {
        agent.isStopped = false;
        anim.SetBool("attackBool", false);
        anim.SetBool("walkBool", true);
    }

    private void AnimAttack()
    {
        agent.isStopped = true;
        anim.SetBool("walkBool", false);
        anim.SetBool("attackBool",true);
    }
    private IEnumerator AnimWait()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(wait);
        agent.isStopped = false;
        anim.SetBool("walkBool", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsDeath)
        {
            if (other.gameObject.CompareTag("PathFinish"))
            {
                transform.DOMove(MapManager.instance.tower.position, 1).OnComplete(() =>
                {
                    Destroy(this.gameObject);
                });

            }

            if (other.gameObject.CompareTag("Bullet"))
            {
                var gun = other.gameObject.GetComponent<Bullet>();
                CoinManager coin = gameObject.GetComponent<CoinManager>();

                if (this.HealtMove - gun.Damage > 0)
                {
                    this.HealtMove -= gun.Damage;
                }
                else
                {
                    this.IsDeath = true;
                    //Debug.Log("Trigger : " + IsDeath);
                    if (isDeath) 
                    { 
                        setDeath();
                        string enemyName = this.gameObject.name.ToString();
                        Debug.Log("Ölen Düşman : " + enemyName);
                        
                        coin.Count += coin.EnemyCoinCount;
                        Debug.Log("EnemyScript para miktarı : " + coin.Count);
                    }
                }
                Destroy(other.gameObject); 
            }
        }
    }

    private void setDeath()
    {
        if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            StartCoroutine(DisableNavMeshAgent());
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is not active or not on a NavMesh.");
        }
        anim.SetTrigger("deathTrig");
        Destroy(gameObject, 5f);
    }

    private IEnumerator DisableNavMeshAgent()
    {
        yield return null;

        if (agent != null)
        {
            agent.enabled = false;
        }
    }

    //public void damageControl(float damage, float armor, float magicResistance) // Zırh ve Büyü direnci gibi kontroller"
    //{
    //    float arm = armor - this.Armor;
    //    float mag = magicResistance - this.MagicResistance;

    //    if (armor > this.Armor)
    //        this.HealtMove -= damage + (damage * arm) / 100;
    //    else
    //    {
    //        if (damage + arm < this.Armor)
    //        {
    //            float yuzde = (damage * arm) / 100;
    //            this.HealtMove -= yuzde;
    //        }
    //        else
    //            this.HealtMove -= damage - arm;
    //    }

    //    if (magicResistance > this.MagicResistance)
    //        this.HealtMove -= damage + (damage * mag) / 100;
    //    else
    //    {
    //        if (damage + mag < this.MagicResistance)
    //        {
    //            float yuzde = (damage * mag) / 100;
    //            this.HealtMove -= yuzde;
    //        }
    //        else
    //            this.HealtMove -= damage - mag;
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

    public bool IsDeath
    {
        get { return isDeath; }
        set { isDeath = value; }
    }
}