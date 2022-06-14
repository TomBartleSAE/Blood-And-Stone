using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToCastleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<VampireModel>())
        {
            GameManager.Instance.CallPhaseChange("DayTest", "NightTest", GameManager.Instance.dayPhaseState);
        }
    }
}
