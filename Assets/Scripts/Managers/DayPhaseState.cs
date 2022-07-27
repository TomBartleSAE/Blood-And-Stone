using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayPhaseState : StateBase
{
    private SoldierSpawner _soldierSpawner;
    public float timeBeforeVillagersSpawn = 15f;

    public override void Enter()
    {
        base.Enter();

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
    }
    
    public IEnumerator EndDay()
    {
        MessageManager.Instance.ShowMessage("The villagers are all dead, for now...", 5f);
        yield return new WaitForSeconds(5f);
        GameManager.Instance.CallPhaseChange("NightTest", "DayTest", GameManager.Instance.nightPhaseState);
    }

    public void CallCastleDestroyed()
    {
        StartCoroutine(CastleDestroyed());
    }

    public IEnumerator CastleDestroyed()
    {
        MessageManager.Instance.ShowMessage("The villagers destroyed your castle!", 3f);
        yield return new WaitForSeconds(3f);
        GameManager.Instance.CallPhaseChange("MainMenu", "DayTest", GameManager.Instance.mainMenuState);
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
