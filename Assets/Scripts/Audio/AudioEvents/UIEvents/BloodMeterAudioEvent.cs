using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BloodMeterAudioEvent : MonoBehaviour
{
    //Subscribe to PlayerManager.Instance.BloodChangedEvent (function requires int variable), check if int variable is greater than 0
    private void Start()
    {
        PlayerManager.Instance.BloodChangedEvent += PlaySound;
    }

    private void OnDestroy()
    {
        PlayerManager.Instance.BloodChangedEvent -= PlaySound;
    }

    public void PlaySound(int bloodCount)
    {
        string clipName = "BloodMeter";
        AudioManager.ArrayName arrayName = AudioManager.ArrayName.ui;
        if (bloodCount > 0)
        {
            AudioManager.Instance.Play(clipName, arrayName);
        }
    }

}
