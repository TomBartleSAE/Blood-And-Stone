using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayPhaseState : StateBase
{
    private SoldierSpawner _soldierSpawner;
    public float timeBeforeVillagersSpawn = 15f;

    public event Action WaveStartedEvent;
    public event Action WaveEndedEvent;

    public override void Enter()
    {
        base.Enter();

        finishedSpawning = false;
        
        GameManager.Instance.currentDay++;
        MessageManager.Instance.ShowMessage("Day " + GameManager.Instance.currentDay, 3f);
        
        PlayerManager.Instance.SetSaveData();
        SaveManager.Instance.SaveGame(PlayerManager.Instance.saveData, SaveManager.Instance.saveFilePath);

        _soldierSpawner = SoldierSpawner.Instance;
        _soldierSpawner.FinishedSpawningEvent += FinishedSpawning;

        DayNPCManager.Instance.SoldierDeathEvent += CheckDayEnd;
    }

    public override void Exit()
    {
        base.Exit();
        
        _soldierSpawner.FinishedSpawningEvent -= FinishedSpawning;
    }

    // HACK: This should be using a state machine and change to a separate state
    public bool finishedSpawning = false;
    public void FinishedSpawning()
    {
        finishedSpawning = true;
    }
    
    public void StartWave()
    {
        MessageManager.Instance.ShowMessage("The villagers are here!", 3f);
        
        StartCoroutine(_soldierSpawner.SpawnWaves(GameManager.Instance.currentDay - 1)); // Adjust by 1 to account for array index starting at 0
        
        WaveStartedEvent?.Invoke();
    }
    
    public IEnumerator EndDay()
    {
        WaveEndedEvent?.Invoke();
        MessageManager.Instance.ShowMessage("The villagers are all dead, for now...", 5f);
        yield return new WaitForSeconds(5f);
        GameManager.Instance.CallPhaseChange("NightTest", "DayTest", GameManager.Instance.nightPhaseState);
    }
    
    public void CastleDestroyed()
    {
        DayNPCManager.Instance.GameOverDayEventFired();
        GameManager.Instance.GameOverMessage("The villagers destroyed your castle!");
    }

    public void CheckDayEnd()
    {
        if (finishedSpawning)
        {
            if (DayNPCManager.Instance.Soldiers.Count <= 0)
            {
                StartCoroutine(EndDay());
            }
        }
    }
}
