using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : ManagerBase<AudioManager>
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public AmbienceSound[] ambienceSounds;

	public MenuSound[] menuSounds;

	public MusicSound[] musicSounds;

	public SFXSound[] sfxSounds;

	public PlayerFootstepsSound[] playerFootstepsSounds;

	public VillagerFootstepsSound[] villagerFootstepsSounds;

	public GuardFootstepsSound[] guardFootstepsSounds;

	public GhoulFootstepsSound[] ghoulFootstepsSounds;



	public override void Awake()
	{
		base.Awake();

		foreach (AmbienceSound s in ambienceSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}

		foreach (MenuSound s in menuSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}

		foreach (MusicSound s in musicSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}

		foreach (SFXSound s in sfxSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}

		foreach (PlayerFootstepsSound s in playerFootstepsSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}

		foreach (GuardFootstepsSound s in guardFootstepsSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}

		foreach (VillagerFootstepsSound s in villagerFootstepsSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}

		foreach (GhoulFootstepsSound s in ghoulFootstepsSounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}

	}

	//Playing SFX on call

	public void Play(string sound)
	{
		SFXSound s = Array.Find(sfxSounds, item => item.name == sound);
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

	public void PlayMusic(string sound)
	{
		MusicSound s = Array.Find(musicSounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume;
		s.source.pitch = s.pitch;

		s.source.Play();

	}


	// UI System below
	public void PlayUI(string sound)
	{
		MenuSound s = Array.Find(menuSounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume;
		s.source.pitch = s.pitch;

		s.source.Play();

	}

    public void PlayHover()
    {
		PlayUI("ButtonHover");

    }

    public void PlayClickMain()
    {
		PlayUI("ButtonClickMain");

    }

    public void PlayClickBack()
    {
		PlayUI("ButtonClickBack");

    }

    public void PlayClickHUD()
    {
		PlayUI("ButtonClickHUD");

    }

    public void PlayEnter()
    {
		PlayUI("ButtonEnter");

    }



	//Footsteps System Below

	public void StepPlayer()
	{
		
		int index = UnityEngine.Random.Range(1, playerFootstepsSounds.Length + 1);

		PlayerFootstepsSound s = Array.Find(playerFootstepsSounds, item => item.indexNumber == index);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void StepGuard()
	{

		int index = UnityEngine.Random.Range(1, guardFootstepsSounds.Length + 1);

		GuardFootstepsSound s = Array.Find(guardFootstepsSounds, item => item.indexNumber == index);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void StepVillager()
	{

		int index = UnityEngine.Random.Range(1, villagerFootstepsSounds.Length + 1);

		VillagerFootstepsSound s = Array.Find(villagerFootstepsSounds, item => item.indexNumber == index);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void StepGhoul()
	{

		int index = UnityEngine.Random.Range(1, ghoulFootstepsSounds.Length + 1);

		GhoulFootstepsSound s = Array.Find(ghoulFootstepsSounds, item => item.indexNumber == index);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	//


	//        myAudioSource.PlayDelayed(Random.Range(minDelay, maxDelay)); <<<< This element will be good only for ambience
	//        myAudioSource.Play();
}
