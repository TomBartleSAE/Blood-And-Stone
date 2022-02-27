using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    public enum DayPhase
    {
        Night,
        Day
    }

    public DayPhase dayPhase;

    public float timeSpeedMultiplier;
    public float time;

    public Transform p1Sunrise;
    public Transform p4Sunset;
    public Transform p2;
    public Transform p3;

    public Vector2 point;

    public GameObject sun;
    public GameObject moon;

    public float timeElapsed;
    public float lerpDuration;
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeCycle());
        lerpDuration = (12 * timeSpeedMultiplier);
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // if (timeElapsed < lerpDuration)
        // {
        //     sun.transform.position = Vector2.Lerp(p1Sunrise.position, p4Sunset.position, (timeElapsed/lerpDuration));
        //     timeElapsed += Time.deltaTime;
        // }

        time += Time.deltaTime;

        if (time > timeSpeedMultiplier)
        {
            time = 0;
            timeElapsed += 1;
        }

        Vector2 lerpA = Vector2.Lerp(p1Sunrise.position, p2.position, (timeElapsed/lerpDuration));
        Vector2 lerpB = Vector2.Lerp(p2.position, p3.position, (timeElapsed/lerpDuration));
        Vector2 lerpC = Vector2.Lerp(p3.position, p4Sunset.position, (timeElapsed/lerpDuration));
        Vector2 lerpD = Vector2.Lerp(lerpA, lerpB, (timeElapsed/lerpDuration));
        Vector2 lerpE = Vector2.Lerp(lerpB, lerpC, (timeElapsed/lerpDuration));

        point = Vector2.Lerp(lerpD, lerpE, (timeElapsed/lerpDuration));
        
        if (dayPhase == DayPhase.Day)
        {
            sun.transform.position = point;
        }
        
        else if (dayPhase == DayPhase.Night)
        {
            moon.transform.position = point;
        }

        //Probably extremely long winded way to do this tbh
        if (timeElapsed >= lerpDuration)
        {

            timeElapsed = 0;
            
            if (dayPhase == DayPhase.Day)
            {
                dayPhase = DayPhase.Night;
            }

            else if(dayPhase == DayPhase.Night)
            {
                dayPhase = DayPhase.Day;
            }
        }
    }

    //Doesn't seem to want to function properly
    //Was used to run time smoothly
    public IEnumerator TimeCycle()
    {
        for (int i = 1; i < 13; i++)
        {
            time = 1 * timeSpeedMultiplier;
            yield return new WaitForSeconds(time);
            
            Debug.Log("Time = " + i);
        }

        StartCoroutine(TimeCycle());
    }
}
