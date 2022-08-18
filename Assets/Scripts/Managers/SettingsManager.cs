using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsManager : ManagerBase<SettingsManager>
{
    public float volumeLevel;
    public bool tutorialCompleted;
    
    private void Start()
    {
        LoadSettings();
        AudioListener.volume = volumeLevel;
        //tutorialCompleted = TutorialManager.Instance.tutorialPreviouslyCompleted;
    }

    private void OnApplicationQuit()
    {
	    SaveSettings();
    }

    //0 == true; 1 == false;
    public bool IntToBool(int value)
    {
        if (value == 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public int BoolToInt(bool value)
    {
        if (value)
        {
            return 0;
        }

        else
        {
            return 1;
        }
    }
    

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("volume", volumeLevel);
        PlayerPrefs.SetInt("tutorial", BoolToInt(tutorialCompleted));
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        volumeLevel = PlayerPrefs.GetFloat("volume");
        //gets previous tutorial status ie completed or not
        int tutorialStatus = PlayerPrefs.GetInt("tutorial");
        tutorialCompleted = IntToBool(tutorialStatus);
    }
}
