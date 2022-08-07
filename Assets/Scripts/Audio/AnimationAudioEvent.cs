using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudioEvent : MonoBehaviour
{
	[HideInInspector]
	public AudioManager.ArrayName arrayName;

	[HideInInspector]
	public string clipName;


	public void PlayFootstep(string character)
	{
		int index = 0;
		switch (character)
		{
			case "Player":
				arrayName = AudioManager.ArrayName.playerfootsteps;
				index = UnityEngine.Random.Range(0, AudioManager.Instance.playerFootstepsSounds.Length);
				clipName = AudioManager.Instance.playerFootstepsSounds[index].soundName;
				break;
			case "Villager":
				arrayName = AudioManager.ArrayName.villagerfootsteps;
				index = UnityEngine.Random.Range(0, AudioManager.Instance.villagerFootstepsSounds.Length);
				clipName = AudioManager.Instance.villagerFootstepsSounds[index].soundName;
				break;
			case "Ghoul":
				arrayName = AudioManager.ArrayName.ghoulfootsteps;
				index = UnityEngine.Random.Range(0, AudioManager.Instance.ghoulFootstepsSounds.Length);
				clipName = AudioManager.Instance.ghoulFootstepsSounds[index].soundName;
				break;
			case "Guard":
				arrayName = AudioManager.ArrayName.guardfootsteps;
				index = UnityEngine.Random.Range(0, AudioManager.Instance.guardFootstepsSounds.Length);
				clipName = AudioManager.Instance.guardFootstepsSounds[index].soundName;
				break;
		}

		AudioManager.Instance.Play(clipName, arrayName);

	}


	// so, we need a switch function for scream, death start and end (grunt, body drop) pc bite, pc captured, pc death, guard death, guard capture pc, guard conversion 1+2, ghoul death




	//20 - 22 = npc attack sounds

	public void PlayAttack()
	{
		int index = UnityEngine.Random.Range(20, 22);
		arrayName = AudioManager.ArrayName.sfx;
		clipName = AudioManager.Instance.playerFootstepsSounds[index].soundName;

		AudioManager.Instance.Play(clipName, arrayName);

	}
}