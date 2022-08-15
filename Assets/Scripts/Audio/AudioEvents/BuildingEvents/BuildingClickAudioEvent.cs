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
        TooltipObject.SelectedObjectEvent += PlayClick;
    }

    public void OnDestroy()
    {
        TooltipObject.SelectedObjectEvent -= PlayClick;
    }

    public void PlayClick()
    {
        AudioManager.Instance.Play(clipName, arrayName);
    }


}
