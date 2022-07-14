using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject buttons;
    public GameObject screens;
    
    void ShowScreens(bool value)
    {
        buttons.SetActive(value);
    }

    void ShowButtons(bool value)
    {
        screens.SetActive(value);
    }

    public void QuitGame()
    {

        //for testing
        UnityEditor.EditorApplication.isPlaying = false;
        //for build
        //Application.Quit();
    }
    
}
