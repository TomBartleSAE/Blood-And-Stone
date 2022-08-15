using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDestroyedAudioEvent : MonoBehaviour
{
    public BuildingBase buildingBase;
    public string clipName;
    public AudioManager.ArrayName arrayName;

    void OnEnable()
    {
        buildingBase.BuildingDestroyedEvent += PlayDestroyedAudio;
    }


    void OnDisable()
    {
        buildingBase.BuildingDestroyedEvent -= PlayDestroyedAudio;
    }

    public void PlayDestroyedAudio()
    {
        AudioManager.Instance.Play(clipName, arrayName);
    }
}
