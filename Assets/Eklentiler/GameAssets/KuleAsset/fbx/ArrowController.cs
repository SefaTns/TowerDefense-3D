using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 10f; // Okun hýzý
    private Transform target;  // Hedef (Düþman)

    // Oku hedefe yönlendirme
    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position; // Hedef ile ok arasýndaki yön
        float distanceThisFrame = speed * Time.deltaTime;    // O anki karede gidilecek mesafe

        // Hedefe ulaþtýysa
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Hedefe doðru ilerle
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        // Oku hedefe doðru döndür, yerel eksende döndürme uygulamak için Quaternion kullan
        Quaternion targetRotation = Quaternion.LookRotation(dir);

        // Belirli eksenlerde döndürmeyi uygulayýn (Yatay veya dikey sapmayý düzeltmek için)
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    // Hedefe çarptýðýnda
    void HitTarget()
    {
        Destroy(gameObject); // Ok yok edilir (burada düþmana hasar verebilirsiniz)
    }
}
