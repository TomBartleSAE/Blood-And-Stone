using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundManager : MonoBehaviour
{

    public AudioManager.ArrayName arrayName;



    public void PlaySound(string clipName)
    {
        AudioManager.Instance.Play(clipName, arrayName);
    }

 //   public void TestSound()
 //   {
 //       FindObjectOfType<AudioManager>().Play("BloodMeter", AudioManager.ArrayName.ui);
 //   }

}