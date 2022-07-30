using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : ManagerBase<AudioManager>
{
	public AudioMixerGroup mixerGroup;

	public SoundData[] ambienceSounds;

	public SoundData[] menuSounds;

	public SoundData[] musicSounds;

	public SoundData[] sfxSounds;

	public SoundData[] playerFootstepsSounds;

	public SoundData[] villagerFootstepsSounds;

	public SoundData[] guardFootstepsSounds;

	public SoundData[] ghoulFootstepsSounds;

	public enum ArrayName {
	
	ambience, 
	ui,
	music,
	sfx,
	playerfootsteps,
	villagerfootsteps,
	guardfootsteps,
	ghoulfootsteps
	}



	public override void Awake()
	{
		base.Awake();

		foreach (SoundData s in ambienceSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
			if (s.playWhenPaused == true)						// this is going to make sure a sound can play when the game is paused :)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in menuSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in musicSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in sfxSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in playerFootstepsSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in guardFootstepsSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in villagerFootstepsSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

		foreach (SoundData s in ghoulFootstepsSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
			if (s.playWhenPaused == true)
			{
				s.source.ignoreListenerPause = true;
			}
		}

	}

	//Playing SFX on call

	public void Play(string sound, ArrayName arrayName)
	{
		SoundData s=new SoundData();
		switch (arrayName)
		{
			case ArrayName.ambience:
				s = Array.Find(ambienceSounds, item => item.soundName == sound);
				break;
			case ArrayName.ui:
				s = Array.Find(menuSounds, item => item.soundName == sound);
				break;
			case ArrayName.music:
				s = Array.Find(musicSounds, item => item.soundName == sound);
				break;
			case ArrayName.sfx:
				s = Array.Find(sfxSounds, item => item.soundName == sound);
				break;
			case ArrayName.playerfootsteps:
				s = Array.Find(playerFootstepsSounds, item => item.soundName == sound);
				break;
			case ArrayName.villagerfootsteps:
				s = Array.Find(villagerFootstepsSounds, item => item.soundName == sound);
				break;
			case ArrayName.guardfootsteps:
				s = Array.Find(guardFootstepsSounds, item => item.soundName == sound);
				break;
			case ArrayName.ghoulfootsteps:
				s = Array.Find(ghoulFootstepsSounds, item => item.soundName == sound);
				break;
		}
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));  //I believe this is randomising the volume/pitch by allocating a random number from the volume variance number -/+ 2
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}








	//Music System below

	//public void PlayMusic(string sound)
	//{
	//	MusicSound s = Array.Find(musicSounds, item => item.name == sound);
	//	if (s == null)
	//	{
	//		Debug.LogWarning("Sound: " + name + " not found!");
	//		return;
	//	}

	//	s.source.volume = s.volume;
	//	s.source.pitch = s.pitch;

	//	s.source.Play();

	//}


	//// UI System below
	//public void PlayUI(string sound)
	//{
	//	MenuSound s = Array.Find(menuSounds, item => item.name == sound);
	//	if (s == null)
	//	{
	//		Debug.LogWarning("Sound: " + name + " not found!");
	//		return;
	//	}

	//	s.source.volume = s.volume;
	//	s.source.pitch = s.pitch;

	//	s.source.Play();

	//}

 //   public void PlayHover()
 //   {
	//	PlayUI("ButtonHover");

 //   }

 //   public void PlayClickMain()
 //   {
	//	PlayUI("ButtonClickMain");

 //   }

 //   public void PlayClickBack()
 //   {
	//	PlayUI("ButtonClickBack");

 //   }

 //   public void PlayClickHUD()
 //   {
	//	PlayUI("ButtonClickHUD");

 //   }

 //   public void PlayEnter()
 //   {
	//	PlayUI("ButtonEnter");

 //   }



	////Footsteps System Below

	//public void StepPlayer()
	//{
		
	//	int index = UnityEngine.Random.Range(1, playerFootstepsSounds.Length + 1);

	//	PlayerFootstepsSound s = Array.Find(playerFootstepsSounds, item => item.indexNumber == index);
	//	if (s == null)
	//	{
	//		Debug.LogWarning("Sound: " + name + " not found!");
	//		return;
	//	}

	//	s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
	//	s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

	//	s.source.Play();
	//}

	//public void StepGuard()
	//{

	//	int index = UnityEngine.Random.Range(1, guardFootstepsSounds.Length + 1);

	//	GuardFootstepsSound s = Array.Find(guardFootstepsSounds, item => item.indexNumber == index);
	//	if (s == null)
	//	{
	//		Debug.LogWarning("Sound: " + name + " not found!");
	//		return;
	//	}

	//	s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
	//	s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

	//	s.source.Play();
	//}

	//public void StepVillager()
	//{

	//	int index = UnityEngine.Random.Range(1, villagerFootstepsSounds.Length + 1);

	//	VillagerFootstepsSound s = Array.Find(villagerFootstepsSounds, item => item.indexNumber == index);
	//	if (s == null)
	//	{
	//		Debug.LogWarning("Sound: " + name + " not found!");
	//		return;
	//	}

	//	s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
	//	s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

	//	s.source.Play();
	//}

	//public void StepGhoul()
	//{

	//	int index = UnityEngine.Random.Range(1, ghoulFootstepsSounds.Length + 1);

	//	GhoulFootstepsSound s = Array.Find(ghoulFootstepsSounds, item => item.indexNumber == index);
	//	if (s == null)
	//	{
	//		Debug.LogWarning("Sound: " + name + " not found!");
	//		return;
	//	}

	//	s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
	//	s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

	//	s.source.Play();
	//}

	//


	//        myAudioSource.PlayDelayed(Random.Range(minDelay, maxDelay)); <<<< This element will be good only for ambience, will need mindelay/maxdelay variables, maybe add those to array for ambience?
}
