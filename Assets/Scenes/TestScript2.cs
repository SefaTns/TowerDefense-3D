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
        //// Y eksenini korumak için rotationu hesapla
        //Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.forward);
        //// Nesnenin dönüþünü ayarla
        //transform.rotation = targetRotation;

        Vector3 direction = target.position - hip.position;
        // Nesnenin customUp vektörünü hedefe doðrult
        Quaternion targetRotation = Quaternion.LookRotation(direction, customUp);
        // Nesnenin dönüþünü ayarla
        transform.rotation = targetRotation;


    }

}
