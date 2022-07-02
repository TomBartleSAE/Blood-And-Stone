using System;
using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEngine;
using UnityEngine.UI;

public class HealthMeterUI : MonoBehaviour
{
    public Slider slider;
    public Health health;

    public void OnEnable()
    {
        health.DamageChangeEvent += UpdateUI;
        health.MaxHealthChangedEvent += UpdateUI;
        UpdateUI();
    }

    public void OnDisable()
    {
        health.DamageChangeEvent -= UpdateUI;
        health.MaxHealthChangedEvent -= UpdateUI;
    }
    
    // Need 2 function to be able to subscribe to both health events
    // TODO: Consider making health events send same data
    public void UpdateUI(GameObject a)
    {
        slider.value = health.currentHealth / health.MaxHealth;
    }
    
    public void UpdateUI()
    {
        slider.value = health.currentHealth / health.MaxHealth;
    }
}
