using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettingsUI : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle muteToggle;

    public float previousVolume;
    
    enum ScreenResolution
    {
        A,
        B,
        C
    }

    private float defaultVolume = 1;
    private bool defaultMute = false;
    
    ScreenResolution screenResSelection(ScreenResolution resolution)
    {
        if (resolution == ScreenResolution.A)
        {
            Screen.SetResolution(1920, 1080, false);
        }

        if (resolution == ScreenResolution.B)
        {
            Screen.SetResolution(1280, 720, false);
        }

        if (resolution == ScreenResolution.C)
        {
            Screen.SetResolution(1280, 1040, false);
        };
        return resolution;
    }

    private void FixedUpdate()
    {
        //changes volume according to slider
        AudioListener.volume = volumeSlider.value;
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
        }
    }

}
