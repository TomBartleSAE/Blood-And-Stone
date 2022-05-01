using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : ManagerBase<GameManager>
{
    public int currentDay;
    public StateBase dayPhaseState, nightPhaseState;
    public StateManager stateManager;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
    
    public void ChangeToDayPhase()
    {
        SceneManager.LoadScene("DayTest");
        stateManager.ChangeState(dayPhaseState);
    }

    public void ChangeToNightPhase()
    {
        SceneManager.LoadScene("NightTest");
        stateManager.ChangeState(nightPhaseState);
    }
}
