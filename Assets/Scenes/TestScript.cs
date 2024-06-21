using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private LayerMask hitlayer;
    private RaycastHit hit;
    [SerializeField] private Transform body;
    private float nextAttackTime = 0f;

    private float damage = 20f;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 0.22f;
        Debug.DrawRay(transform.position, forward, Color.red, 1.0f);

        if (Physics.Raycast(body.position, forward, out hit, 0.22f, hitlayer))
        {
            if (hit.collider.CompareTag("ShieldDoor"))
            {
                TestScript2 healt = hit.transform.GetComponent<TestScript2>();
                if (healt.healt >= 0)
                {
                    anim.SetBool("attackBool", true);

                    AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
                    float animationDuration = stateInfo.length;
                    nextAttackTime = Time.time + animationDuration;

                    Debug.Log(healt.healt);
                    healt.healt -= damage;
                    Debug.Log(hit.collider.name);
                }
                else
                {
                    anim.SetBool("attackBool", false);
                    anim.SetBool("walkBool", true);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
