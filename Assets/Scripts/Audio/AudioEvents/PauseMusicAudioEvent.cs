using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMusicAudioEvent : MonoBehaviour
{
    public PauseSystem pauseSystem;
    [HideInInspector]
    public string clipName;
    [HideInInspector]
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
        switch (AudioManager.Instance.currentPhase)
        {
            case "DayTest":
                clipName = "DayPhasePause";
                break;
            case "NightTest":
                clipName = "NightPhasePause";
                break;
            case "Tutorial_Act1-1":
                clipName = "NightPhasePause";
                break;
            case "Tutorial_Act1-2":
                clipName = "NightPhasePause";
                break;
            case "Tutorial_Act2-1":
                clipName = "DayPhasePause";
                break;
            case "Tutorial_Act2-2":
                clipName = "DayPhasePause";
                break;
        }

        AudioManager.Instance.PausePlaying();
        AudioManager.Instance.Play(clipName, arrayName);
    }

    // set up for the tutorial phases as well
    // DayTest, NightTest, Tutorial_Act1-1 1-2 2-1 2-2

    public void StopPauseAudio()
    {
        switch (AudioManager.Instance.currentPhase)
        {
            case "DayTest":
                clipName = "DayPhasePause";
                break;
            case "NightTest":
                clipName = "NightPhasePause";
                break;
            case "Tutorial_Act1-1":
                clipName = "NightPhasePause";
                break;
            case "Tutorial_Act1-2":
                clipName = "NightPhasePause";
                break;
            case "Tutorial_Act2-1":
                clipName = "DayPhasePause";
                break;
            case "Tutorial_Act2-2":
                clipName = "DayPhasePause";
                break;
        }
        AudioManager.Instance.StopPlaying(clipName);
        AudioManager.Instance.ResumePlaying();
    }

}