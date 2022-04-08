using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodMeterUI : MonoBehaviour
{
    public Blood blood;
    public Slider slider;

    public void Awake()
    {
        blood.BloodChangedEvent += UpdateUI;
        blood.MaxBloodChangedEvent += UpdateUI;
    }

    public void UpdateUI(int amount)
    {
        float bloodPercentage = (float)blood.currentBlood / (float)blood.maxBlood;
        slider.value = bloodPercentage;
    }
}
