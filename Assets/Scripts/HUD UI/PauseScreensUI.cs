using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreensUI : MonoBehaviour
{
    public GameObject buttonsGroup;
    public GameObject loadGame;
    public GameObject howToPlay;
    public GameObject settings;
    public GameObject quit;

    public void ShowLoadGame(bool value)
    {
        loadGame.SetActive(value);
    }

    public void ShowHowToPlay(bool value)
    {
        howToPlay.SetActive(value);
    }

    public void ShowSettings(bool value)
    {
        settings.SetActive(value);
    }

    public void ShowQuit(bool value)
    {
        quit.SetActive(value);
    }

    public void ShowButtonsGroup(bool value)
    {
        buttonsGroup.SetActive(value);
    }

    public void LoadGame()
    {
        
    }
}
