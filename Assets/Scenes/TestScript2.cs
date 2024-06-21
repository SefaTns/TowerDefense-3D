using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    public float healt;
    public Transform target;
    public Transform hip;
    public float rotationSpeed = 5f;

    public Vector3 customUp = Vector3.forward;

    private void Update()
    {
        //Vector3 direction = target.position - transform.position;
        //Quaternion targetRotation = Quaternion.LookRotation(direction);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //Vector3 direction = target.position - transform.position;
        //// Y eksenini korumak i�in rotationu hesapla
        //Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.forward);
        //// Nesnenin d�n���n� ayarla
        //transform.rotation = targetRotation;

        Vector3 direction = target.position - hip.position;
        // Nesnenin customUp vekt�r�n� hedefe do�rult
        Quaternion targetRotation = Quaternion.LookRotation(direction, customUp);
        // Nesnenin d�n���n� ayarla
        transform.rotation = targetRotation;


    }

}
