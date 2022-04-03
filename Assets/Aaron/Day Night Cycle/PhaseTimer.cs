using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseTimer : MonoBehaviour
{
    public event Action PhaseEndEvent;
    public GameObject sunMoon;
    public AnimationCurve arc;
    public Transform sunRise;
    public Transform sunSet;

    //real time passed
    public float time;
    //duration in seconds phase will last for
    public float phaseLength;
    //adjusts height of object arc in game view; adjustable depending on level layout
    public float heightMultiplier;
    //converts the phase length to a 0-1 value relating to the curve
    public float phaseLengthNormalised;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        phaseLengthNormalised = Mathf.InverseLerp(0, phaseLength, time);

        //not sure why/how I did this tbh; it was ages ago
        float yPos = arc.Evaluate(time / phaseLength);
        //xPos is distance * normalised value
        float xPos = (Vector3.Distance(sunRise.position, sunSet.position) * phaseLengthNormalised);

        //todo lerp/dotween the movement to smooth it out
        //moves sun/moon according to time passed and corresponding y value of animation curve
        sunMoon.transform.position = new Vector3(sunRise.position.x + xPos, yPos * heightMultiplier);
        
        //should fire off an event when phase is complete
        if (time >= phaseLength)
        {
            PhaseEndEvent?.Invoke();
        }
    }

    public IEnumerator Timer()
    {
        time = 0;
        
        while (time < phaseLength)
        {
            time++;

            yield return new WaitForSeconds(1);
        }
        
        StopCoroutine(Timer());
    }
}
