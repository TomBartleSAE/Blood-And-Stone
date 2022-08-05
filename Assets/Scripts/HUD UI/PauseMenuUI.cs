using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        
        //for build
        #if !UNITY_EDITOR
        Application.Quit();
        #endif
    }
    
    public void Load()
    {
        if (SaveManager.Instance.SaveFileExists(SaveManager.Instance.saveFilePath))
        {
            PlayerManager.Instance.LoadSaveData(SaveManager.Instance.LoadGame(SaveManager.Instance.saveFilePath));
            GameManager.Instance.CallPhaseChange("DayTest", SceneManager.GetActiveScene().name, GameManager.Instance.dayPhaseState);
        }
    }
}
