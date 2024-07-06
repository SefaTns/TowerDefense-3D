using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private Transform[] spawnTransform;
    int spawnIndex = 0;

    public float spawnRate = 1.0f;
    public float timeBeetwenWaves = 4.0f;

    public int enemyCount;
    private int k = 0;

    bool waveIsDone = true;
    private string [] enemyNames;

    private static int enemySpawnCount = 0;


    public void Start()
    {
       enemyNames  = new string [enemyPrefabs.Length];

        for(int i =0; i < enemyPrefabs.Length; i++)
        {
            switch (enemyPrefabs[i].GetComponent<EnemyScript>().name)
            {
                case "Knight":
                    enemyPrefabs[i].GetComponent<EnemyScript>().healthMove = 50.0f;
                    break;

                case "Rouge":
                    enemyPrefabs[i].GetComponent<EnemyScript>().healthMove = 30.0f;
                    break;

                case "Mage":
                    enemyPrefabs[i].GetComponent<EnemyScript>().healthMove = 30.0f;
                    break;
                default:
                    enemyPrefabs[i].GetComponent<EnemyScript>().healthMove = 20.0f;
                    break;
            }
        }

    }

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
        string enemy;
        yield return new WaitForSeconds(timeBeetwenWaves);
        //Debug.Log(enemyPrefabs[k].name+" geliyor...");
        enemy = enemyPrefabs[k].name;

        if (!enemyNames.Contains(enemy))
        {
            for(int a = 0; a < enemyNames.Length; a++)
            {

                if (enemyNames[a] == null)
                {
                    enemyNames[a] = enemy;
                    break;
                }
            }
            int tempIndex = spawnIndex;
            spawnIndex = Random.Range(0, spawnTransform.Length);

            while (spawnIndex != tempIndex)
            {
                spawnIndex = Random.Range(0, spawnTransform.Length);
            }
            // Debug.Log(spawnIndex);
            //Debug.Log("bu düşman daha önce gelmedi");
            for (int i = 0; i < enemyCount; i++)
            {
                EnemySpawnCount++;
                GameObject enemyClone = Instantiate(enemyPrefabs[k], spawnTransform[spawnIndex].position, Quaternion.identity);
                yield return new WaitForSeconds(spawnRate);
            }
            Debug.Log("Güncel düşman sayısı : " + EnemySpawnCount);
        }
        else
        {
            //Debug.Log("bu düşman daha önce geldi");
            enemyPrefabs[k].GetComponent<EnemyScript>().healthMove += 10.0f;
            GameObject enemyClone = Instantiate(enemyPrefabs[k], spawnTransform[spawnIndex].position, Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);
        }
        //Debug.Log(enemyPrefabs[k].name + enemyPrefabs[k].GetComponent<EnemyScript>().healthMove);
        k++;
        spawnRate -= 0.1f;
        yield return new WaitForSeconds(timeBeetwenWaves + 3);
        
        waveIsDone = true;
    }

    //private bool EnemyControl()
    //{
    //    GameObject[] enemyControl = GameObject.FindGameObjectsWithTag("Enemy");

    //    if (enemyControl.Length > 0) return false;

    //    else return true;
         
    //}

    public int EnemySpawnCount
    {
        get { return enemySpawnCount; }
        set { enemySpawnCount = value; }
    }
}
