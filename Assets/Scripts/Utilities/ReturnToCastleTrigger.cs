using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReturnToCastleTrigger : MonoBehaviour
{
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
}
