using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBloodMeter : MonoBehaviour
{

    [HideInInspector]
    public AudioManager audioManager;

    [HideInInspector]
    public BloodMeterUI bloodMeter;

    [HideInInspector]
    public float oldValue;

    [HideInInspector]
    public float newValue;


    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();
        bloodMeter = GetComponent<BloodMeterUI>();
        oldValue = bloodMeter.slider.value;
    }

    public void UpdateValue()
    {
        //newValue = bloodMeter.slider.value;
    }

    public void PlaySound()
    {
        if(newValue > oldValue)
    
        audioManager.PlayUI("BloodMeter");
    }

}
