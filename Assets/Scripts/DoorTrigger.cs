using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private float doorHealt;
    private bool doorControl = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            this.DoorControl = true;
            Vector3 currentRotation = transform.eulerAngles;


            if(this.DoorHealt == 0)
            {
                if (this.gameObject.name == "left_door")
                {
                    currentRotation.y = 90f;
                }
                else
                    currentRotation.y = -90f;
            }
            
            transform.eulerAngles = currentRotation;
        }
        else
            this.DoorControl = false;
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
