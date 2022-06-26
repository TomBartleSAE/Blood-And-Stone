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
    }

    public void OnDisable()
    {
        health.DamageChangeEvent -= UpdateUI;
        health.MaxHealthChangedEvent -= UpdateUI;
    }

    public void UpdateUI(GameObject a)
    {
        slider.value = health.currentHealth / health.MaxHealth;
    }
    
    public void UpdateUI()
    {
        slider.value = health.currentHealth / health.MaxHealth;
    }
}
