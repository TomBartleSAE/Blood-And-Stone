using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Add transforms to this list when new spawn points become available
    public List<Transform> spawnPoints = new List<Transform>();
    
    public Wave[] waves;
    
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
                Instantiate(currentGroup.enemy, currentSpawn.position + positionOffset, Quaternion.identity);
            }

            yield return new WaitForSeconds(currentGroup.timeToNextGroup);
            groupIndex++;
        }
    }
}
