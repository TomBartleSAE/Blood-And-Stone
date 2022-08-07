using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMusicAudioEvent : MonoBehaviour
{
    public PauseSystem pauseSystem;
    public string clipName;
    public AudioManager.ArrayName arrayName;

    public void Start()
    {
        pauseSystem.GamePausedEvent += PlayPauseAudio;
        pauseSystem.GameResumedEvent += StopPauseAudio;
    }

    public void OnDestroy()
    {
        pauseSystem.GamePausedEvent -= PlayPauseAudio;
        pauseSystem.GameResumedEvent -= StopPauseAudio;
    }

    public void PlayPauseAudio()
    {
        arrayName = AudioManager.ArrayName.music;
        if (AudioManager.currentPhase = "dayPhase")
        {
            clipName = "DayPhasePause";
        }
        else if (AudioManager.currentPhase = "nightPhase")
        {
            clipName = "NightPhasePause";
        }
        AudioManager.Instance.PausePlaying();
        AudioManager.Instance.Play(clipName, arrayName);
    }

    public void StopPauseAudio()
    {
        if (AudioManager.currentPhase = "dayPhase")
        {
            clipName = "DayPhasePause";
        }
        else if (AudioManager.currentPhase = "nightPhase")
        {
            clipName = "NightPhasePause";
        }
        AudioManager.Instance.StopPlaying(clipName);
        AudioManager.Instance.ResumePlaying();
    }

}