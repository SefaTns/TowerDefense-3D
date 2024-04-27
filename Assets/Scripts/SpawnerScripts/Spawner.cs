using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefabs;
    [SerializeField] private Transform spawnTransform;
    
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), 0, Random.Range(0, 3f));
    }

    private void Spawn()
    {
        Instantiate(enemyPrefabs, spawnTransform.position, Quaternion.identity);
    }
}
