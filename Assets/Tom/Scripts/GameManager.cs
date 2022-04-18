using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentDay;
    
    public Spawner spawner;

    public float timeBeforeVillagersSpawn = 15f;

    public void Start()
    {
        StartCoroutine(StartWave());
    }

    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBeforeVillagersSpawn);
        
        MessageManager.Instance.ShowMessage("The villagers are here!", 3f);
        
        StartCoroutine(spawner.SpawnWaves(currentDay));
    }
}
