using System;
using System.Collections;
using System.Collections.Generic;
using Tanks;
using UnityEngine;

public class GhoulSpawner : MonoBehaviour
{
    public GameObject thingToSpawn;
    public Transform locationToSpawn;
    public int amountToSpawn;
    
    public event Action<GameObject> GhoulSpawnedEvent;  
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnGhouls();
    }
    
    void SpawnGhouls()
    {
        GameObject copy = thingToSpawn;
        for (int i = 0; i < amountToSpawn; i++)
        {
            Instantiate(copy, locationToSpawn.position, copy.transform.rotation);
            GhoulSpawnedEvent?.Invoke(copy);        
        }
    }
}
