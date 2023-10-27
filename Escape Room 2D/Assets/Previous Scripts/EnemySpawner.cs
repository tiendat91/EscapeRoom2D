using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private float spawnRadius = 3.5f, time = 1.5f;

    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    private IEnumerator spawnEnemy()
    {
        Vector2 spawnPos = GameObject.Find("Hero").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

        Instantiate(enemies[Random.Range(0, 9)], spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(time);
        StartCoroutine(spawnEnemy());
    }
}
