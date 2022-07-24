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
        audioManager.PlayUI("ButtonHover");

    }

    public void PlayClickMain()
    {
        audioManager.PlayUI("ButtonClickMain");

    }

    public void PlayClickBack()
    {
        audioManager.PlayUI("ButtonClickBack");

    }

    public void PlayClickHUD()
    {
        audioManager.PlayUI("ButtonClickHUD");

    }

    public void PlayEnter()
    {
        audioManager.PlayUI("ButtonEnter");

    }

}
