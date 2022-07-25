using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GhoulIconUI : MonoBehaviour
{
    public GameObject eyesOpen;
    public GameObject eyesClosed;

    public BoxSelection boxSelection;

    public TextMeshProUGUI popcapText;

    public bool isDayPhase;

    private void Start()
    {
        PlayerManager.Instance.CurrentGhoulsChangedEvent += UpdateGhoulCount;
        PlayerManager.Instance.MaxGhoulsChangedEvent += UpdateGhoulCount;
        UpdateGhoulCount(0);
        if (isDayPhase)
        {
            boxSelection.GhoulSelectedEvent += SetEyes;
        }

    }
    
    //changes icon view
    public void SetEyes(bool value)
    {
        eyesOpen.SetActive(value);
        eyesClosed.SetActive(!value);
    }
    
    private void UpdateGhoulCount(int value)
    {
        popcapText.text = PlayerManager.Instance.CurrentGhouls + "/" + PlayerManager.Instance.GhoulPopcap;
    }
}
