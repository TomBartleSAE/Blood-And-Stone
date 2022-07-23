using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenuSound : MonoBehaviour
{
    [HideInInspector]
    public AudioManager audioManager;



    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();


    }


    public void PlayHover()
    {
        audioManager.Play("ButtonHover");

    }

    public void PlayClick()
    {
        audioManager.Play("ButtonClick");

    }

    public void PlayEnter()
    {
        audioManager.Play("ButtonEnter");

    }

}
