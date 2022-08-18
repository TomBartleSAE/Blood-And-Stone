using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAudioEvent : MonoBehaviour
{
    public GuardModel guardModel;
    public string alertClipName;
    public string uiAlertClipName;
    public string ghoulClipName;
    public AudioManager.ArrayName arrayName;

    // if wanted, can get sub to NotAlertedEvent for when guard loses sight of vampire

    void OnEnable()
    {
        guardModel.AlertedEvent += PlayAlertAudio;
        guardModel.GhoulConversionEvent += PlayGhoulAudio;
    }


    void OnDisable()
    {
        guardModel.AlertedEvent -= PlayAlertAudio;
        guardModel.GhoulConversionEvent -= PlayGhoulAudio;
    }

    public void PlayAlertAudio()
    {
        AudioManager.Instance.Play(uiAlertClipName, arrayName);
        AudioManager.Instance.Play(alertClipName, arrayName);
    }
    public void PlayGhoulAudio()
    {
        int index = UnityEngine.Random.Range(1, 4);
        AudioManager.Instance.Play(ghoulClipName + index, arrayName);
    }
}
