using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : ManagerBase<LevelTimer>
{
    public bool timerActive = false;
    public float totalTime;
    public float timer;
    public float nearlyOverEventTime = 10f;
    private bool nearlyOverEventTriggered = false;

    public event Action TimerFinishedEvent;
    public event Action TimerNearlyOverEvent;

    private void Update()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;

            if (timer <= nearlyOverEventTime && !nearlyOverEventTriggered)
            {
                TimerNearlyOverEvent?.Invoke();
                nearlyOverEventTriggered = true;
            }
            
            if (timer <= 0)
            {
                timerActive = false;
                TimerFinishedEvent?.Invoke();
            }
        }
    }

    public void StartTimer(float seconds)
    {
        totalTime = seconds;
        timer = totalTime;
        timerActive = true;
    }
}
