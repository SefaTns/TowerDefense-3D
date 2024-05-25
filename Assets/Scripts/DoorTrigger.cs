using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private float doorHealt;
    private bool doorControl = false;
    private bool canTrigger = true;
    private float waitTime = 2.3f;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Enemy"))
    //    {
    //        Debug.Log(this.DoorHealt);
    //        this.DoorControl = true;
    //        Vector3 currentRotation = transform.eulerAngles;
    //        Debug.Log(this.doorControl);

    //        var enemyDamage = other.gameObject.GetComponent<EnemyScript>();
    //        Debug.Log(enemyDamage.Damage);

    //        this.DoorHealt -= enemyDamage.Damage;

    //        if(this.DoorHealt == 0)
    //        {
    //            if (this.gameObject.name == "left_door")
    //            {
    //                currentRotation.y = -90f;
    //            }
    //            else
    //                currentRotation.y = 90f;
    //        }
            
    //        transform.eulerAngles = currentRotation;
    //    }
    //    else
    //        this.DoorControl = false;
    //}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && canTrigger)
        {
            Vector3 currentRotation = transform.eulerAngles;
            var enemyDamage = other.gameObject.GetComponent<EnemyScript>();
            if (this.DoorHealt != 0)
            {
                StartCoroutine(TriggerCooldown());
                Debug.Log("Kapý Can : " + this.DoorHealt);
                

                Debug.Log("Düþman hasar : "+enemyDamage.Damage);

                this.DoorHealt -= enemyDamage.Damage;
            }
            if(this.DoorHealt == 0)
            {
                if (this.gameObject.name == "left_door")
                {
                    currentRotation.y = -90f;
                }
                else
                    currentRotation.y = 90f;
            }

            transform.eulerAngles = currentRotation;
        }
        
    }

    private IEnumerator TriggerCooldown()
    {
        canTrigger = false;
        yield return new WaitForSeconds(waitTime);
        canTrigger = true;
    }


    public float DoorHealt
    {
        get { return this.doorHealt; }
        set { this.doorHealt = value; }
    }

    public bool DoorControl
    {
        get { return this.doorControl; }
        set { this.doorControl = value; }
    }
}
