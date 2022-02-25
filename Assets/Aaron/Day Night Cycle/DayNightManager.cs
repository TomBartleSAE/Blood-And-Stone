using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{

    public float timeSpeedMultiplier;
    public float time;

    public Transform sunRise;
    public Transform sunSet;

    public GameObject sun;

    public float timeElapsed;
    public float lerpDuration;
    


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TimeCycle());
        lerpDuration = (12 * timeSpeedMultiplier);
    }

    // Update is called once per frame
    void Update()
    {

        if (timeElapsed < lerpDuration)
        {
            sun.transform.position = Vector3.Lerp(sunRise.position, sunSet.position, (timeElapsed/lerpDuration));
            timeElapsed += Time.deltaTime;
        }
    }

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
