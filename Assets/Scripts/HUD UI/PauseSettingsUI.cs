using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettingsUI : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle muteToggle;
    public TMP_Dropdown dropdown;
    
    List<string> resOptions = new List<string>() {"1920 x 1080", "1280 x 720", "1280 x 1040"};

    public float previousVolume;

    private float defaultVolume = 1;
    private bool defaultMute = false;

    private void Start()
    {
        PopulateResolutionList();
    }

    public void RestoreDefaultSettings()
    {
	    MuteAudio(defaultMute);
	    ChangeVolume(defaultVolume);
	    volumeSlider.value = defaultVolume;
        ResolutionChanged(0);
        dropdown.value = 0;
    }

    public void ChangeVolume(float newVolume)
    {
	    AudioListener.volume = newVolume;
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
        }
    }

    void PopulateResolutionList()
    {
	    dropdown.AddOptions(resOptions);
    }

    public void ResolutionChanged(int indexValue)
    {
        //HACK probably could do with a more solid reference to size?
        if (indexValue == 0)
        {
            Screen.SetResolution(1920, 1080, false);
        }

        if (indexValue == 1)
        {
            Screen.SetResolution(1280, 720, false);
        }

        if (indexValue == 2)
        {
            Screen.SetResolution(1280, 1040, false);
        }
        Debug.Log(Screen.currentResolution);
    }

}
