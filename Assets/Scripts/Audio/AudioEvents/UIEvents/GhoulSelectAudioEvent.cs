using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulSelectAudioEvent : MonoBehaviour
{
    public BoxSelection boxSelection;
    public string clipName;
    public AudioManager.ArrayName arrayName;

    void OnEnable()
    {
        boxSelection.GhoulSelectedEvent += PlayClickAudio;
    }


    void OnDisable()
    {
        boxSelection.GhoulSelectedEvent -= PlayClickAudio;
    }

    public void PlayClickAudio(bool ghoulTargeted)
    {
        if (ghoulTargeted)
        {
            AudioManager.Instance.Play(clipName, arrayName);
        }

    }
}
