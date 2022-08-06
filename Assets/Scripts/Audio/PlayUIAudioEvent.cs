using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUIAudioEvent : MonoBehaviour
{

    public AudioManager.ArrayName arrayName;



    public void PlaySound(string clipName)
    {
        AudioManager.Instance.Play(clipName, arrayName);
    }

 //   the following code is what needs to be placed within other scripts to call the play function in the audio manager
 //
 //   public void TestSound()
 //   {
 //       FindObjectOfType<AudioManager>().Play("Insert clip name in inspector with quote marks", AudioManager.ArrayName."insert arrayname without quote marks");
 //   }

}