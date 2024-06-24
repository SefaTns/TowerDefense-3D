using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private float doorHealt;

    public float DoorHealt
    {
        get { return this.doorHealt; }
        set { this.doorHealt = value; }
    }
}
