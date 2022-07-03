using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertIndicator : MonoBehaviour
{
    public GameObject eyeOpen;
    public GameObject eyeClosed;

    private void OnEnable()
    {
        foreach (var guard in NightNPCManager.Instance.Guards)
        {
            guard.GetComponent<GuardModel>().AlertedEvent += OpenEyeIcon;
            guard.GetComponent<GuardModel>().NotAlertedEvent += CloseEyeIcon;
        }
    }

    private void OnDisable()
    {
        foreach (var guard in NightNPCManager.Instance.Guards)
        {
            guard.GetComponent<GuardModel>().AlertedEvent -= OpenEyeIcon;
            guard.GetComponent<GuardModel>().NotAlertedEvent -= CloseEyeIcon;
        }
    }

    void OpenEyeIcon()
    {
        eyeOpen.SetActive(true);
        eyeClosed.SetActive(false);
    }

    void CloseEyeIcon()
    {
        eyeClosed.SetActive(true);
        eyeOpen.SetActive(false);
    }
}
