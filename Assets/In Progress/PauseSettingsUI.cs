using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseSettingsUI : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle muteToggle;

    public float previousVolume;

    private float defaultVolume = 100;
    private bool defaultMute = false;

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
            muteToggle.isOn = false;
        }
    }

}
