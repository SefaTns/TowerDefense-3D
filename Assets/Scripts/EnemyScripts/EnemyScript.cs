using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using Unity.VisualScripting;
using System.Linq;

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

    bool kapı = false;
    Animator [] attack; 

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        attack = new Animator [10];
        anim = GetComponent<Animator>();
        StartCoroutine(AnimWait());
        agent.SetDestination(MapManager.instance.tower.position);
    }
    private void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (KapıKontrol())
            {
                hareket();
            }
            else
            {
                agent.isStopped = false;
                anim.SetBool("attackBool", false);
                anim.SetBool("walkBool", true);
            }
        }
    }

    private bool KapıKontrol()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 0.22f;

        if (Physics.Raycast(body.position, forward, out hit, 0.22f, hitlayer))
        {

            if (hit.collider.CompareTag("ShieldDoor"))
            {
                
                foreach (int i in Enumerable.Range(0, attack.Length))
                {
                    if (attack[i] == null)
                    {
                        attack[i] = this.anim;
                        break;
                    }
                }
                DoorTrigger doorComp = hit.transform.GetComponent<DoorTrigger>();
                Debug.Log(doorComp.DoorHealt);
                if (doorComp.DoorHealt >= 0)
                {
                    doorComp.DoorHealt -= damage;
                    kapı = true;
                }
                else
                {
                    Destroy(hit.collider.gameObject);
                    kapı = false;
                }
            }
            else
            {
                kapı = false;
            }
        }
        return kapı;
    }

    public void hareket()
    {
        
        foreach (int i in Enumerable.Range(0, attack.Length))
        {
            if (attack[i] != null)
            {

                agent.isStopped = true;
                attack[i].SetBool("attackBool", true);
                AnimatorStateInfo stateInfo = attack[i].GetCurrentAnimatorStateInfo(0);
                float animationDuration = stateInfo.length;
                nextAttackTime = Time.time + animationDuration;


                //    agent.isStopped = true;
                //    attack[i].SetBool("walkBool", false);
                //    attack[i].SetBool("attackBool", true);
            }
        }
    }


    //private void Attack()
    //{
    //    if (!this.IsDeath)
    //    {
    //        Vector3 forward = transform.TransformDirection(Vector3.forward) * 0.22f;
    //        //Debug.DrawRay(transform.position, forward, Color.red, 1.0f);

    //        if (Physics.Raycast(body.position, forward, out hit, 0.22f, hitlayer))
    //        {

    //            if (hit.collider.CompareTag("ShieldDoor") && kapı)
    //            {
    //                DoorTrigger doorComp = hit.transform.GetComponent<DoorTrigger>();
    //                Debug.Log(doorComp.DoorHealt);
    //                if (doorComp.DoorHealt >= 0)
    //                {
    //                    agent.isStopped = true;
    //                    anim.SetBool("attackBool", true);
    //                    AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
    //                    float animationDuration = stateInfo.length;
    //                    nextAttackTime = Time.time + animationDuration;

    //                    doorComp.DoorHealt -= damage;
    //                }
    //                else
    //                { 
    //                    if(hit.collider.CompareTag("ShieldDoor"))
    //                    {
    //                        Destroy(hit.collider.gameObject);
    //                        Debug.Log("kapı kırıldı düsman yüricek");
    //                    }
    //                    Anim_Agent_Set();
    //                }
    //            }
    //        }
    //    }
    //}

    private void Anim_Agent_Set()
    {
        agent.isStopped = false;
        anim.SetBool("attackBool", false);
        anim.SetBool("walkBool", true);
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

                if (this.HealtMove - gun.Damage > 0)
                {
                    this.HealtMove -= gun.Damage;
                }
                else
                {
                    this.IsDeath = true;
                    //Debug.Log("Trigger : " + IsDeath);
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