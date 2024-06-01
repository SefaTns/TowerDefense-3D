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
    private bool isDeath = false;

    private Animator anim;
    private NavMeshAgent agent;
    private float wait = 1f;

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
        if (!isDeath)
        {
            if (other.gameObject.CompareTag("leftDoor") || other.gameObject.CompareTag("rightDoor"))
            {
                agent.isStopped = true;
                anim.SetBool("attackBool", true);

                var doorComp = other.gameObject.GetComponent<DoorTrigger>();
                if (doorComp.DoorHealt <= 0)
                {
                    agent.isStopped = false;
                    anim.SetBool("attackBool", false);
                    anim.SetBool("walkBool", true);
                }
            }
        }
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
        if (!isDeath)
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
                var gun = other.gameObject.GetComponent<Bullet>();

                if (this.HealtMove - gun.Damage > 0)
                {
                    this.HealtMove -= gun.Damage;
                }
                else
                {
                    this.IsDeath = true;
                    Debug.Log("Trigger : " + IsDeath);
                    if (isDeath) { setDeath(); }
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

    public void damageControl(float damage, float armor, float magicResistance) // Zırh ve Büyü direnci gibi kontroller"
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