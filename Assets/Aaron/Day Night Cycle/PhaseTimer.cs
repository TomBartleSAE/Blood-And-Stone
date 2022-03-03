using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTimer : MonoBehaviour
{
    public event Action PhaseEndEvent;
    public GameObject sunMoon;
    public AnimationCurve arc;
    public Transform sunRise;

    public float testValue;

    public float time;
    //0-1 value of animation curve; varies depending on phaselength variable 
    public float arcTime;
    
    //duration in seconds phase will last for
    public float phaseLength;
    //adjusts height of object arc in game view
    public float heightMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        time += Time.deltaTime;

        //gets x value along animation curve
        arcTime = arc.Evaluate(time / phaseLength);

        //moves sun/moon according to time passed and corresponding y value of animation curve
        sunMoon.transform.position = new Vector3(sunRise.transform.position.x + time, arcTime * heightMultiplier);
        
        //should fire off an event when phase is complete
        if (time >= phaseLength)
        {
            PhaseEndEvent?.Invoke();
        }
    }
}
