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
        
        timer.TimerFinishedEvent -= SunriseGameOver;
    }
    
     public void SunriseGameOver()
     {
         NightNPCManager.Instance.GameOverEventFired();
         GameManager.Instance.GameOverMessage("The sun has risen and you are burned to ashes...");
     }
}
