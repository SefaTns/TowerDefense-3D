using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private float doorHealt;
    //private bool canTrigger = true;
    //private float waitTime = 2.4f;
    //public Slider slider;

    //public void Start()
    //{
    //    slider.maxValue = doorHealt;
    //    slider.value = doorHealt;
    //}


    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Enemy") && canTrigger)
    //    {
    //        StartCoroutine(TriggerCooldown());
    //        var enemyDamage = other.gameObject.GetComponent<EnemyScript>();

    //        this.DoorHealt -= enemyDamage.Damage;

    //        if (this.DoorHealt <= 0)
    //        {
    //            if (this.gameObject.CompareTag("leftDoor") || this.gameObject.CompareTag("rightDoor"))
    //            {
    //                Destroy(this.gameObject);
    //            }
    //        }
    //    }
    //}

    //public IEnumerator TriggerCooldown()
    //{
    //    canTrigger = false;
    //    yield return new WaitForSeconds(waitTime);
    //    canTrigger = true;
    //}


    public float DoorHealt
    {
        get { return this.doorHealt; }
        set { this.doorHealt = value; }
    }
}
