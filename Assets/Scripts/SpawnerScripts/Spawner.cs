using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform spawnTransform;

    public float spawnRate = 1.0f;
    public float timeBeetwenWaves = 2.0f;

    public int enemyCount;
    private int k = 0;

    bool waveIsDone = true;

    private void Update()
    {
        if(waveIsDone && k < enemyPrefabs.Length)
        {
            StartCoroutine(waveSpawner());
        }
    }

    IEnumerator waveSpawner()
    {
        waveIsDone = false;

        yield return new WaitForSeconds(timeBeetwenWaves);

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyClone = Instantiate(enemyPrefabs[k], spawnTransform.position, Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);
        }
        k++;
        spawnRate -= 0.1f;
        
        yield return new WaitForSeconds(timeBeetwenWaves);
        
        waveIsDone = true;
    }

    private bool EnemyControl()
    {
        GameObject[] enemyControl = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemyControl.Length > 0) return false;

        else return true;
         
    }
}
