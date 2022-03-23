using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int numberToSpawn = 1;
    public Transform goal;
    public PathfindingGrid grid;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position + offset, transform.rotation);
            newEnemy.GetComponent<PathfindingAgent>().destination = goal;
            newEnemy.GetComponent<PathfindingAgent>().grid = grid;
        }
    }
}
