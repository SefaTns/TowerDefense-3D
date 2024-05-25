using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BarbarAnimScript : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("walkBool", true);
        agent.SetDestination(MapManager.instance.tower.position);
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("leftDoor") || other.gameObject.CompareTag("rightDoor"))
        {
            agent.isStopped = true;
            //anim.SetBool("walkBool", false);
            anim.SetBool("attackBool", true);
        }
        var doorComp = other.gameObject.GetComponent<DoorTrigger>();
        if(doorComp.DoorHealt == 0)
        {
            agent.isStopped = false;
            anim.SetBool("walkBool", true) ;
        }
    }


}
