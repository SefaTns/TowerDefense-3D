﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealtBarController : MonoBehaviour
{
    //private int enemyCount = 0;
    public float doorHealth;
    public Slider slider;
    public GameObject yildiz3;
    public GameObject yildiz2;
    public GameObject yildiz1;
    public GameObject lose;

    private static int enemyInCount = 0; // Kaleye giren düşman sayısı

    public void Start()
    {
        slider.maxValue = doorHealth;
        slider.value = doorHealth;
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Triggered");
        if (other.gameObject.CompareTag("Enemy") && doorHealth >= 0)
        {
            //Debug.Log("Enemy hit door");
            EnemyInCount++;
            ApplyDamage(other);
        }
        else
        {
            TriggerCooldown();
            Time.timeScale = 0;
            lose.SetActive(true);
        }
    }

    private IEnumerator TriggerCooldown()
    {
        yield return new WaitForSeconds(3f);
    }

    private void ApplyDamage(Collider enemy)
    {
        //Debug.Log("slider bar updated");
        var enemyDamage = enemy.gameObject.GetComponent<EnemyScript>();
        doorHealth -= enemyDamage.Damage;
        slider.value = doorHealth;
        //enemyCount++;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Oyunun hızını normale döndür
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Mevcut sahneyi yeniden yükler
    }

    //public int EnemyCount
    //{
    //    get { return enemyCount; }
    //    set { enemyCount = value; }
    //}

    public int EnemyInCount
    {
        get { return enemyInCount; }
        set { enemyInCount = value; }
    }
}
