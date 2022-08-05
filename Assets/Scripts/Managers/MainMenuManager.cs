using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI continueText;
    
    public Slider volumeSlider;
    public Toggle muteToggle;

    public float previousVolume;

    private float defaultVolume = 100;
    private bool defaultMute = false;
    //TODO need screensize when added

    
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
        GameManager.Instance.CallPhaseChange("Tutorial_Act1-1", "MainMenu", GameManager.Instance.tutorialState);
    }

    public void Load()
    {
        if (SaveManager.Instance.SaveFileExists(SaveManager.Instance.saveFilePath))
        {
            PlayerManager.Instance.LoadSaveData(SaveManager.Instance.LoadGame(SaveManager.Instance.saveFilePath));
            GameManager.Instance.CallPhaseChange("DayTest", SceneManager.GetActiveScene().name, GameManager.Instance.dayPhaseState);
        }
    }

    public void RestoreDefaultSettings()
    {
        AudioListener.volume = defaultVolume;
        MuteAudio(defaultMute);
        //TODO restore to default resolution
    }

    public void MuteAudio(bool value)
    {
        if (value)
        {
            previousVolume = AudioListener.volume;
            volumeSlider.value = 0;
        }

        if (!value)
        {
            volumeSlider.value = previousVolume;
            muteToggle.isOn = false;
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
