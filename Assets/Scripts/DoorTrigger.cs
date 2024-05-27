using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private float doorHealt;
    private bool canTrigger = true;
    private float waitTime = 2.4f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && canTrigger)
        {
            StartCoroutine(TriggerCooldown());
            Vector3 currentRotation = this.transform.eulerAngles;
            
            var enemyDamage = other.gameObject.GetComponent<EnemyScript>();

            this.DoorHealt -= enemyDamage.Damage;
            Debug.Log("Kapý Can : " + this.DoorHealt);

            if (this.DoorHealt <= 0)
            {
                if (this.gameObject.CompareTag("leftDoor"))
                {
                    currentRotation.y = -90f;
                }
                if (this.gameObject.CompareTag("rightDoor"))
                    currentRotation.y = 90f;
            }

            this.transform.eulerAngles = currentRotation;
            Debug.Log("Current : " + currentRotation.ToString());
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
}
