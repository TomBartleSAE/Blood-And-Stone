using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDialUI : MonoBehaviour
{
    private LevelTimer timer;
    public Transform dial;

    private void Start()
    {
        timer = FindObjectOfType<LevelTimer>(); // HACK
    }

    private void Update()
    {
        float timePercentage = (timer.totalTime - timer.timer) / timer.totalTime;
        
        dial.rotation = Quaternion.Euler(0f,0f,180f * timePercentage);
    }
}
