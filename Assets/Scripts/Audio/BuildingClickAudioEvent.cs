using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingClickAudioEvent : MonoBehaviour
{
    public TooltipObject TooltipObject;
    public string clipName;
    public AudioManager.ArrayName arrayName;

    public void Start()
    {
        TooltipObject.SelectedObjectEvent += PlayAudio;
    }

    public void OnDestroy()
    {
        TooltipObject.SelectedObjectEvent -= PlayAudio;
    }

    public void PlayAudio()
    {
        AudioManager.Instance.Play(clipName, arrayName);
    }


}
