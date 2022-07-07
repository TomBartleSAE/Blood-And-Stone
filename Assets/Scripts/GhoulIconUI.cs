using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhoulIconUI : MonoBehaviour
{
    public GameObject eyesOpen;
    public GameObject eyesClosed;

    //Hardcoded to the box select; was hoping to do it via DayNPCManager but alas
    public BoxSelection boxSelection;

    public bool ghoulSelected;

    private void Start()
    {
        boxSelection.GhoulSelectedEvent += EyesOn;
        boxSelection.GhoulNotSelectedEvent += EyesOff;
    }
    
    //ultimately changes images according to selection
    public void EyesOn()
    {
        ghoulSelected = true;
    }

    public void EyesOff()
    {
        ghoulSelected = false;
    }

    //sets which image is present
    private void Update()
    {
        if (ghoulSelected)
        {
            eyesOpen.SetActive(true);
            eyesClosed.SetActive(false);
        }
        else
        {
            eyesClosed.SetActive(true);
            eyesOpen.SetActive(false);
        }
    }
}
