using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacedAudioEvent : MonoBehaviour
{

    public TowerPlacement towerPlacement;
    public string clipName;
    public AudioManager.ArrayName arrayName;

    void OnEnable()
    {
        towerPlacement.BuildingCreatedEvent += PlayPlacedAudio;
    }


    void OnDisable()
    {
        towerPlacement.BuildingCreatedEvent -= PlayPlacedAudio;
    }

    public void PlayPlacedAudio()
    {
        AudioManager.Instance.Play(clipName, arrayName);
    }
}
