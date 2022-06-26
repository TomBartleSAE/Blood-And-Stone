using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodMeterUI : MonoBehaviour
{
    public Slider slider;

    public void Start()
    {
        PlayerManager.Instance.BloodChangedEvent += UpdateUI;
        PlayerManager.Instance.MaxBloodChangedEvent += UpdateUI;
        UpdateUI(0);
    }

    public void OnDestroy()
    {
        PlayerManager.Instance.BloodChangedEvent -= UpdateUI;
        PlayerManager.Instance.MaxBloodChangedEvent -= UpdateUI;
    }

    public void UpdateUI(int amount)
    {
        float bloodPercentage = (float)PlayerManager.Instance.currentBlood / (float)PlayerManager.Instance.maxBlood;
        slider.value = bloodPercentage;
    }
}
