using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float speed = 10f; // Okun h�z�
    private Transform target;  // Hedef (D��man)

    // Oku hedefe y�nlendirme
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

        Vector3 dir = target.position - transform.position; // Hedef ile ok aras�ndaki y�n
        float distanceThisFrame = speed * Time.deltaTime;    // O anki karede gidilecek mesafe

        // Hedefe ula�t�ysa
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Hedefe do�ru ilerle
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

        // Oku hedefe do�ru d�nd�r, yerel eksende d�nd�rme uygulamak i�in Quaternion kullan
        Quaternion targetRotation = Quaternion.LookRotation(dir);

        // Belirli eksenlerde d�nd�rmeyi uygulay�n (Yatay veya dikey sapmay� d�zeltmek i�in)
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }

    // Hedefe �arpt���nda
    void HitTarget()
    {
        Destroy(gameObject); // Ok yok edilir (burada d��mana hasar verebilirsiniz)
    }
}
