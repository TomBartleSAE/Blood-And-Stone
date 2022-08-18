using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : ManagerBase<MainMenuManager>
{
    public TextMeshProUGUI continueText;
    public PauseSettingsUI settings;

    private bool tutorialCompleted;

    private void Start()
    {
        if (SaveManager.Instance.SaveFileExists(SaveManager.Instance.saveFilePath))
        {
            continueText.color = Color.black;
        }
        else
        {
            continueText.color = Color.gray;
        }
    }

    public void Play()
    {
        PlayerManager.Instance.LoadSaveData(SaveManager.Instance.LoadGame(SaveManager.Instance.newGameDataPath));
        if (!SettingsManager.Instance.tutorialCompleted)
        {
            GameManager.Instance.CallPhaseChange("Tutorial_Act1-1", "MainMenu", GameManager.Instance.tutorialState);
        }
        else
        {
            GameManager.Instance.CallPhaseChange("NightTest", "MainMenu", GameManager.Instance.nightPhaseState);
        }
    }

    public void Load()
    {
        if (SaveManager.Instance.SaveFileExists(SaveManager.Instance.saveFilePath))
        {
            PlayerManager.Instance.LoadSaveData(SaveManager.Instance.LoadGame(SaveManager.Instance.saveFilePath));
            GameManager.Instance.CallPhaseChange("DayTest", SceneManager.GetActiveScene().name, GameManager.Instance.dayPhaseState);
        }
    }

    public void Quit()
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
}
