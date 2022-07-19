using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertIndicator : MonoBehaviour
{
    public GameObject eyeOpen;
    public GameObject eyeClosed;

    private void Start()
    {
        NightNPCManager.Instance.GuardAlertEvent += EyeIcon;
    }

    private void OnDestroy()
    {
        NightNPCManager.Instance.GuardAlertEvent -= EyeIcon;
    }

    void EyeIcon(bool value)
    {
        if (value)
        {
            eyeOpen.SetActive(true);
            eyeClosed.SetActive(false);
        }
        
        else
        {
            eyeOpen.SetActive(false);
            eyeClosed.SetActive(true);
        }
    }
}
