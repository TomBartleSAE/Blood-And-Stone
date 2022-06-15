using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : ManagerBase<Spawner>
{
    // Add transforms to this list when new spawn points become available
    public List<Transform> spawnPoints = new List<Transform>();
    
    public Wave[] waves;

    public Transform castleTarget;

    public event Action FinishedSpawningEvent;

    public PathfindingGrid grid;
    
    public IEnumerator SpawnWaves(int waveIndex)
    {
        Wave currentWave = waves[waveIndex];
        int groupIndex = 0;
        
        for (int i = 0; i < currentWave.groups.Length; i++)
        {
            Wave.Group currentGroup = currentWave.groups[groupIndex];
            Transform currentSpawn = spawnPoints[Random.Range(0, spawnPoints.Count)];
            
            for (int j = 0; j < currentGroup.numberToSpawn; j++)
            {
                // Gives each object and random offset to prevent them spawning on top of each other
                Vector3 positionOffset = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(-0.5f, 0.5f));
                GameObject newEnemy = Instantiate(currentGroup.enemy, currentSpawn.position + positionOffset, Quaternion.identity);
                newEnemy.GetComponent<SoldierModel>().castle = castleTarget;
                newEnemy.GetComponent<PathfindingAgent>().grid = grid; // HACK: Find another way to do this
            }

            yield return new WaitForSeconds(currentGroup.timeToNextGroup);
            groupIndex++;
        }
        
        FinishedSpawningEvent?.Invoke();
    }
}
