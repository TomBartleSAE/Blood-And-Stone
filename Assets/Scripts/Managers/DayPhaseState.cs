using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayPhaseState : StateBase
{
    private Spawner spawner;
    public float timeBeforeVillagersSpawn = 15f;

    public override void Enter()
    {
        base.Enter();

        spawner = Spawner.Instance;
        spawner.FinishedSpawningEvent += FinishedSpawning;
        StartCoroutine(StartWave());
    }

    public override void Execute()
    {
        base.Execute();
        
        // HUGE HACK: Should be reacting to soldier death events
        if (finishedSpawning)
        {
            if (NPCManager.Instance.Soldiers.Count <= 0)
            {
                StartCoroutine(EndDay());
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        spawner.FinishedSpawningEvent -= FinishedSpawning;
    }

    // HACK: This should be using a state machine and change to a separate state
    public bool finishedSpawning = false;
    public void FinishedSpawning()
    {
        finishedSpawning = true;
    }
    
    public IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBeforeVillagersSpawn);
        
        MessageManager.Instance.ShowMessage("The villagers are here!", 3f);
        
        StartCoroutine(spawner.SpawnWaves(GameManager.Instance.currentDay - 1)); // Adjust by 1 to account for array index starting at 0
    }
    
    public IEnumerator EndDay()
    {
        MessageManager.Instance.ShowMessage("The villagers are all dead, for now...", 5f);
        yield return new WaitForSeconds(5f);
        //StartCoroutine(GameManager.Instance.ChangeToNightPhase());
    }
}
