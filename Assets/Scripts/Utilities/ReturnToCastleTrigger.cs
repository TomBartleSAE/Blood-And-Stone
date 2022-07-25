using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReturnToCastleTrigger : MonoBehaviour
{
    public Collider collider;
    public GameObject view;

    private void Start()
    {
        SetTrigger(false);
        PlayerManager.Instance.BloodChangedEvent += EnableTrigger;
        PlayerManager.Instance.CurrentGhoulsChangedEvent += EnableTrigger;
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.BloodChangedEvent -= EnableTrigger;
        PlayerManager.Instance.CurrentGhoulsChangedEvent -= EnableTrigger;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VampireModel>())
        {
            GameManager.Instance.CallPhaseChange("DayTest", "NightTest", GameManager.Instance.dayPhaseState);
        }

        //added to remove converted guard from view - AM
        if (other.GetComponent<GuardModel>() && other.GetComponent<GuardModel>().isDead)
        {
            other.gameObject.SetActive(false);
        }
    }

    public void EnableTrigger(int blood)
    {
        SetTrigger(true);
    }

    public void SetTrigger(bool enable)
    {
        collider.enabled = enable;
        view.SetActive(enable);
    }
}
