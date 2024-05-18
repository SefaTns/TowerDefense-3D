using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Vector3 currentRotation = transform.eulerAngles;

            if (this.gameObject.name == "left_door")
            {
                currentRotation.y += 10f;
            }
            else
                currentRotation.y += -10f;
            
            transform.eulerAngles = currentRotation;
        }
    }
}
