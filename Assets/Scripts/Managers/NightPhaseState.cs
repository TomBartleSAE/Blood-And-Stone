using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightPhaseState : StateBase
{
    public LevelTimer timer;
    [Tooltip("Total duration of the Night Phase in seconds")]
    public float nightPhaseTime = 60f;

    public override void Enter()
    {
        base.Enter();

        timer.StartTimer(nightPhaseTime);
        timer.TimerFinishedEvent += SunriseGameOver;
    }

    public override void Exit()
    {
        base.Exit();

        NightNPCManager.Instance.Guards = new List<GameObject>();
        NightNPCManager.Instance.Villagers = new List<GameObject>();
        
        timer.TimerFinishedEvent -= SunriseGameOver;
    }
    
     public void SunriseGameOver()
     {
	     GameManager.Instance.GameOverMessage("The sun has risen and you are burned to ashes...");
     }
}
