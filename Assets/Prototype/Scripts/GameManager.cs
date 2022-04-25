using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : ManagerBase<GameManager>
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

    public IEnumerator EndDay()
    {
        MessageManager.Instance.ShowMessage("The villagers are all dead, for now...", 5f);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }

    public void Awake()
    {
        spawner.FinishedSpawningEvent += FinishedSpawning;
    }

    // HACK: This should be using a state machine and change to a separate state
    public bool finishedSpawning = false;
    public void FinishedSpawning()
    {
        finishedSpawning = true;
    }

    private void Update()
    {
        // HUGE HACK: Should be reacting to soldier death events
        if (finishedSpawning)
        {
            if (NPCManager.Instance.Soldiers.Count <= 0)
            {
                StartCoroutine(EndDay());
            }
        }
    }
}
