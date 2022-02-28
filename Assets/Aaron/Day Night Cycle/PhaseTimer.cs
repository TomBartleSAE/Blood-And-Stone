using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseTimer : MonoBehaviour
{
    public event Action PhaseEndEvent;

    public GameObject sunMoon;
    
    public float time;
    public float phaseLength;

    public AnimationCurve arc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if (time >= phaseLength)
        {
            PhaseEndEvent?.Invoke();
        }
        
        
    }
}
