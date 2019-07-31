//#define ENABLE_AUDIO_MANAGER_TEST // 打开这个宏可以开启调试界面

using System;
using System.Collections.Generic;
using ClientCommon;
using LywGames;
//using UnityEditor;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// 音乐音效管理类, 内部使用对象管理池.
/// 
/// 2015/07/25 Modify YaoYilin
/// 
/// 这次改动的地方比较大，做一个笔记，写一下修改说明
/// 
/// 应策划需求，把场景音乐和音效的声音的音量大小在ConstValue表里配置好，用配置的值。
/// 播放音乐音效的对应方法PlayMusic、PlaySound、Play3DMusic等方法之前传入的音量参数volume没有去掉，默认还保留着，
/// 但是使用的时候使用的是MusicVolume和SoundVolume两个属性值的大小。
/// 
/// MusicVolume和SoundVolume两个属性的取值，默认取数据库中的值，如果取到的值为0，那么就取私有变量musicVolume和soundVolume的值
/// musicVolume和soundVolume的值已经让我在代码里设置成0了，如果想使用这两个值，把数据库中的音量大小设置成大于1的数即可。
/// 
/// 目前使用这些播放声效文件的方法，设置音量大小的方法都被强制失效了。
/// 
/// 这些代码以后有时间再重构一下，现在太乱了。
/// 
/// </summary>
public class AudioManager : AbsManager<AudioManager>
{
	UnityEngine.Transform audioLisnterTransform = null;
	UnityEngine.Camera currentAttatchedCamera = null;
	UnityEngine.Transform currentAttachedCameraTransform = null;

	public delegate void PlayAudioDelegate(object userData);

	private const float SOUND_DEFAULT_VOLUME = 1.0f;
	private const float MUSIC_DEFAULT_VOLUME = 1.0f;

	//private string baseSoundPath = "";
	//private string baseMusicPath = "";
	public bool NeedPlayMusic { get; private set; }
	public bool NeedPlaySound { get; private set; }
    private string backMusicName;   //背景音乐名字

    public void SetPlayMusic(bool play)
    {
        NeedPlayMusic = play;
		PlayerPrefs.SetString(PlayerSaveData.SaveDataKeys.MusicKey,NeedPlayMusic.ToString());
        if (!play)
        {
            StopMusic();
        }
        else
        {
			if (!string.IsNullOrEmpty(backMusicName))
			{
				if(!IsMusicPlaying(backMusicName))
					PlayMusic(backMusicName, true);
			}
        }
    }

    public void SetPlaySound( bool play )
    {
        NeedPlaySound = play;
		NGUITools.EnablePlaySound = play;
		PlayerPrefs.SetString(PlayerSaveData.SaveDataKeys.SoundKey, NeedPlaySound.ToString());
    }

    private bool soundMuted = false;
	/// <summary>
	/// 音效静音开关
	/// </summary>
	public bool SoundMuted
	{
		get { return soundMuted; }
		set
		{
			if (soundMuted != value)
			{
				soundMuted = value;
				UpdateAudioVolume(false);
			}
		}
	}

	/// <summary>
	/// 默认soundVolume为0，目前项目中没有设置音量的功能。SoundVolume赋值的优先级为表中设置的值-->soundVolume-->默认值SOUND_DEFAULT_VOLUME
	/// </summary>
	private float soundVolume = 0f;
	/// <summary>
	/// 音效音量   
	/// </summary>
	public float SoundVolume
	{
		get
		{
			float dbVolume = ConstValue.SoundVolume;
			if (dbVolume != 0f)
				return dbVolume;
			else if (soundVolume != 0f)
				return soundVolume;
			else if (dbVolume > SOUND_DEFAULT_VOLUME && soundVolume == 0f)
			{
				soundVolume = 1f;
				return soundVolume;
			}
			else
				return SOUND_DEFAULT_VOLUME;
		}

		set
		{
			if (soundVolume != value)
			{
				soundVolume = value;
				UpdateAudioVolume(false);
			}
		}
	}

	private float CurrentSoundVolume
	{
		get { return soundMuted ? 0 : SoundVolume; }
	}

	private bool musicMuted = false;
	/// <summary>
	/// 音乐静音开关
	/// </summary>
	public bool MusicMuted
	{
		get { return musicMuted; }
		set
		{
			if (musicMuted != value)
			{
				musicMuted = value;
				UpdateAudioVolume(true);
			}
		}
	}

	private float musicVolume = 0f;
	/// <summary>
	/// 音乐音量
	/// </summary>
	public float MusicVolume
	{
		get
		{
			float dbVolume = ConstValue.MusicVolume;
			if (dbVolume != 0f)
				return dbVolume;
			else if (musicVolume != 0)
				return musicVolume;
			else if (dbVolume > SOUND_DEFAULT_VOLUME && musicVolume == 0f)
			{
				musicVolume = SOUND_DEFAULT_VOLUME;
				return musicVolume;
			}
			else
				return MUSIC_DEFAULT_VOLUME;
		}

		set
		{
			if (musicVolume != value)
			{
				musicVolume = value;
				UpdateAudioVolume(true);
			}
		}
	}

	private float CurrentMusicVolume
	{
		get { return musicMuted ? 0 : MusicVolume; }
	}

	private enum PlayState
	{
		NotStart,
		Fadein,
		Normal,
		FadeOut,
		End,
	}

	private class AudioData
	{
		/// <summary>
		/// 是否是音乐类型
		/// </summary>
		public bool isMusic;

		/// <summary>
		/// 声音资源
		/// </summary>
		public AudioSource audioSource;

		/// <summary>
		/// 创建在第几帧
		/// </summary>
		public float createdFrameCount;

		/// <summary>
		/// 创建的时间, 不受TimeScale影响
		/// </summary>
		public float createdTime;

		/// <summary>
		/// 播放装
		/// </summary>
		public PlayState playState;

		/// <summary>
		/// 音量
		/// </summary>
		public float volume;

		/// <summary>
		/// 用于延迟计算的中间计数
		/// </summary>
		public float delay;

		/// <summary>
		/// 淡入淡出时间
		/// </summary>
		public float fadeTime;

		/// <summary>
		/// 淡入淡出开始时间
		/// </summary>
		public float fadeStartTime;

		/// <summary>
		/// 淡入淡出当前音效
		/// </summary>
		public float fadeVolume;

		/// <summary>
		/// 播放回调
		/// </summary>
		public PlayAudioDelegate endDel;
		public object userData;

		public bool IsMusic
		{
			get { return isMusic; }
		}

		public float CurrentVolume
		{
			get { return playState == PlayState.Fadein || playState == PlayState.FadeOut ? fadeVolume : volume; }
		}

		public void Reset()
		{
			isMusic = false;
			audioSource.clip = null;
			audioSource.volume = 0;
			audioSource.ignoreListenerPause = false;
			audioSource.ignoreListenerVolume = false;
			audioSource.loop = false;
			createdFrameCount = 0;
			createdTime = 0;
			playState = PlayState.NotStart;
			volume = 0;
			delay = 0;
			fadeTime = 0;
			fadeStartTime = 0;
			fadeVolume = 0;
			endDel = null;
			userData = null;
		}

		public void UpdateVolume(float globalVolume)
		{
			if (audioSource == null)
				return;

			audioSource.volume = globalVolume;// * CurrentVolume;
		}
	}

	private AudioListener audioListener;
	private Dictionary<string, AudioData> persistentAudios = new Dictionary<string, AudioData>();
	private List<AudioData> spawnedAudio = new List<AudioData>();
	private List<AudioData> despawnedAudio = new List<AudioData>();
	private List<AudioData> audioTempList = new List<AudioData>();

    public void StopAllMusic()
    {
        foreach (var data in persistentAudios.Values)
        {
            data.audioSource.Stop();
        }
    }
	public override void Initialize(params object[] parameters)
	{
		base.Initialize(parameters);


		// Create audio listener
		//audioListener = gameObject.GetComponent<AudioListener>();
		//if (audioListener == null)
		//	audioListener = gameObject.AddComponent<AudioListener>();
		AudioListener.volume = 1;

        //从本地配表初始化音效开关状态
        string temp = PlayerPrefs.GetString(PlayerSaveData.SaveDataKeys.MusicKey);
		NeedPlayMusic = string.IsNullOrEmpty(temp) || StrParser.ParseBool(temp);
		temp = PlayerPrefs.GetString(PlayerSaveData.SaveDataKeys.SoundKey);
        NeedPlaySound = string.IsNullOrEmpty(temp) || StrParser.ParseBool(temp);
		NGUITools.EnablePlaySound = NeedPlaySound;

		LoggerManager.Instance.Info("loading audio lisnter ");
		var audioLisnter = ResourceManager.Instance.InstantiateAsset<GameObject>(AssetType.Other, "AudioListener", false);
        LoggerManager.Instance.Info("finish loading audio lisnter " + (audioLisnter == null ? "null" : "success")); 
		GameObject.DontDestroyOnLoad(audioLisnter); // 没有非空判断，死就死吧
		audioLisnterTransform = audioLisnter.transform;

	}

	public override void Dispose()
	{
		//Destroy(audioListener);

		foreach (var item in spawnedAudio)
			Destroy(item.audioSource.gameObject);
		spawnedAudio.Clear();

		foreach (var item in despawnedAudio)
			Destroy(item.audioSource.gameObject);
		despawnedAudio.Clear();

		foreach (var item in persistentAudios)
			Destroy(item.Value.audioSource.gameObject);
		persistentAudios.Clear();
	}

	public void FreeMemory()
	{
		foreach (var item in despawnedAudio)
			Destroy(item.audioSource.gameObject);
		despawnedAudio.Clear();
	}

	public void RemoveSound(string audioName)
	{
		if (persistentAudios.ContainsKey(audioName))
		{
			Destroy(persistentAudios[audioName].audioSource.gameObject);
			persistentAudios.Remove(audioName);
		}
	}

	public void RemoveSound(List<string> audioNames)
	{
		foreach (string name in audioNames)
			if (persistentAudios.ContainsKey(name))
			{
				Destroy(persistentAudios[name].audioSource.gameObject);
				persistentAudios.Remove(name);
			}
	}

	/// <summary>
	/// 预加载声音文件
	/// </summary>
	/// <param name="audioName">声音名称</param>
	/// <param name="volume">声音音量</param>
	/// <param name="ignoreListenerPause"></param>
	/// <param name="ignoreListenerVolume"></param>
	public void PreLoadingAudioResources(string audioName, float volume = 1, bool ignoreListenerPause = false, bool ignoreListenerVolume = false)
	{
		GetPersistentSound(audioName, volume, ignoreListenerPause, ignoreListenerVolume);
	}

	/// <summary>
	/// 可以预加载声音文件
	/// </summary>
	/// <returns></returns>
	public AudioSource GetPersistentSound(string audioName, float volume, bool ignoreListenerPause, bool ignoreListenerVolume)
	{
		if (persistentAudios.ContainsKey(audioName))
			return persistentAudios[audioName].audioSource;

		var audioClip = ResourceManager.Instance.LoadAsset<AudioClip>(AssetType.Sound, audioName);
		if (audioClip != null)
			audioClip.name = audioName;
		else
			return null;

		var audioData = CreateAudioData();
		audioData.audioSource.clip = audioClip;
		audioData.audioSource.ignoreListenerPause = ignoreListenerPause;
		audioData.audioSource.ignoreListenerVolume = ignoreListenerVolume;
		audioData.audioSource.loop = false;
		audioData.volume = SoundVolume;

		audioData.UpdateVolume(CurrentSoundVolume);

		persistentAudios.Add(audioName, audioData);

		return audioData.audioSource;
	}


	public void GetPersistentSound(List<string> audioNames, float volume = 1, bool ignoreListenerPause = false, bool ignoreListenerVolume = false)
	{
		foreach (string name in audioNames)
			GetPersistentSound(name, volume, ignoreListenerPause, ignoreListenerVolume);
	}

	/// <summary>
	/// 可以预加载声音文件
	/// </summary>
	/// <param name="audioName"></param>
	/// <returns></returns>
	public AudioSource GetPersistentSound(string audioName)
	{
		return GetPersistentSound(audioName, 1, false, false);
	}

	public bool IsSoundPlaying(string soundName)
	{
		for (int i = 0; i < spawnedAudio.Count; ++i)
		{
			var item = spawnedAudio[i];
			if (item.IsMusic == false
				&& item.playState != PlayState.FadeOut
				&& item.playState != PlayState.End
				&& item.audioSource.clip.name.Equals(soundName, StringComparison.CurrentCultureIgnoreCase))
				return true;
		}

		return false;
	}

//#if UNITY_EDITOR
//	[MenuItem("Tools/PlaySelectedAudioSource %3")]
//	public static void PlayEditor()
//	{
//		AudioSource source = null;
//		if (Selection.activeGameObject != null)
//			source = Selection.activeGameObject.GetComponent<AudioSource>();
//
//		if (source == null)
//			source = FindObjectOfType<AudioSource>();
//
//		if (source != null)
//			source.Play();
//	}
//
//	[MenuItem("Tools/StopSelectedAudioSource")]
//	public static void StopEditor()
//	{
//		Selection.activeGameObject.GetComponent<AudioSource>().Stop();
//	}
//#endif

	public void Play3DSound(AudioClip audioClip, Vector3 worldPosition, float volume, float delay, float minDistance = 1, float maxDistance = 500, PlayAudioDelegate endDel = null, object userData = null)
	{
		if (audioClip == null)
			return;
	    
	    AudioData audioData;
		if (persistentAudios.ContainsKey( audioClip.name))
			audioData = persistentAudios[audioClip.name];
		else
			audioData = SpawnAudio();

		audioData.isMusic = false;
		audioData.audioSource.clip = audioClip;
		audioData.audioSource.ignoreListenerPause = false;
		audioData.audioSource.ignoreListenerVolume = false;
		audioData.audioSource.loop = false;
		//Logarithmic效果会更好，然而Unity不能在代码中控制衰减曲线，用Logarithmic不管多远都能听到声音。
		//http://forum.unity3d.com/threads/changing-audiosources-roll-off-via-scripting.70705/#post-2132241
		//使用Linear替代。
		audioData.audioSource.rolloffMode = AudioRolloffMode.Linear;
		audioData.audioSource.spatialBlend = 1;
		audioData.audioSource.transform.position = worldPosition;
		audioData.audioSource.minDistance = minDistance;
		audioData.audioSource.maxDistance = maxDistance;
		audioData.createdFrameCount = Time.frameCount;
		audioData.createdTime = Time.time;
		audioData.volume = SoundVolume;//= volume;
		audioData.delay = delay;
		audioData.endDel += endDel;
		audioData.userData = userData;

		audioData.UpdateVolume(CurrentSoundVolume);

        if (!NeedPlaySound)
        {
            return;
        }

		audioData.audioSource.PlayDelayed(delay);
	}

	public void Play3DSound(string audioName, Vector3 worldPosition, float volume, float delay, float minDistance = 1, float maxDistance = 500)
	{
		AudioSource audioSource = null;

		if (persistentAudios.ContainsKey(audioName))
			audioSource = persistentAudios[audioName].audioSource;

		AudioClip audioClip = null;
		if (audioSource != null)
			audioClip = audioSource.clip;
		else
		{
			audioClip = ResourceManager.Instance.LoadAsset<AudioClip>(AssetType.Sound, audioName);
			if (audioClip != null)
			{
				AudioData audioData = CreateAudioData();
				audioClip.name = audioName;
				persistentAudios.Add(audioName, audioData);
			}
		}

		Play3DSound(audioClip, worldPosition, volume, delay, minDistance: minDistance, maxDistance: maxDistance);
	}

	private bool IsPreLoadSound(string audioName)
	{
		return persistentAudios.ContainsKey(audioName);
	}
	public void PlaySound(AudioClip audioClip, float volume, bool ignoreListenerPause, bool ignoreListenerVolume,
						  float delay, PlayAudioDelegate endDel, object userData)
	{
		if (audioClip == null)
			return;
       

		if (IsPreLoadSound(audioClip.name))
		{
			persistentAudios[audioClip.name].audioSource.volume = SoundVolume;//= volume; 目前改成参数volume无效，使用属性，从表中读取音量大小
			persistentAudios[audioClip.name].audioSource.ignoreListenerPause = ignoreListenerPause;
			persistentAudios[audioClip.name].audioSource.ignoreListenerVolume = ignoreListenerVolume;
			persistentAudios[audioClip.name].audioSource.PlayDelayed(delay);

			if (endDel != null)
				endDel.Invoke(userData);

			return;
		}

		// Combine the sound played in the same frame
		var batchingSound = GetBatchingSound(audioClip, delay, Time.frameCount, userData);
		if (batchingSound != null)
		{
			batchingSound.volume = Mathf.Max(SoundVolume, batchingSound.volume);//Mathf.Max(volume, batchingSound.volume);
			batchingSound.endDel += endDel;

			batchingSound.UpdateVolume(CurrentSoundVolume);
		}
		else
		{
			var audioData = SpawnAudio();
			audioData.isMusic = false;
			audioData.audioSource.clip = audioClip;
			audioData.audioSource.ignoreListenerPause = ignoreListenerPause;
			audioData.audioSource.ignoreListenerVolume = ignoreListenerVolume;
			audioData.audioSource.loop = false;
			audioData.audioSource.spatialBlend = 0;
			audioData.createdFrameCount = Time.frameCount;
			audioData.createdTime = Time.time;
			audioData.volume = SoundVolume;//= volume;
			audioData.delay = delay;
			audioData.endDel += endDel;
			audioData.userData = userData;

			audioData.UpdateVolume(CurrentSoundVolume);

            if (!NeedPlaySound)   //播放声音接口太多 先加这 有空这整理一下
            {
                return;
            }

			audioData.audioSource.PlayDelayed(delay);
		}
	}

	//public void PlaySound(AudioClip audioClip, float volume, float delay, PlayAudioDelegate endDel, object userData)
	//{
	//	PlaySound(audioClip, volume, false, false, delay, endDel, userData);
	//}

	//public void PlaySound(AudioClip audioClip, float volume, float delay)
	//{
	//	PlaySound(audioClip, volume, delay, null, null);
	//}

	//public void PlaySound(AudioClip audioClip, float delay)
	//{
	//	PlaySound(audioClip, 1, delay, null, null);
	//}

	public void PlaySound(string audioName, float volume = SOUND_DEFAULT_VOLUME, bool ignoreListenerPause = false, bool ignoreListenerVolume = false,
						  float delay = 0, PlayAudioDelegate endDel = null, object userData = null)
	{
		AudioSource audioSource = null;
		if (persistentAudios.ContainsKey(audioName))
			audioSource = persistentAudios[audioName].audioSource;
        
		AudioClip audioClip = null;
		if (audioSource != null)
			audioClip = audioSource.clip;
		else
			audioClip = ResourceManager.Instance.LoadAsset<AudioClip>(AssetType.Sound, audioName);

		if (audioClip != null)
			audioClip.name = audioName;
		PlaySound(audioClip, volume, ignoreListenerPause, ignoreListenerVolume, delay, endDel, userData);
	}

	//public void PlaySound(string soundName, float volume, float delay, PlayAudioDelegate endDel, object userData)
	//{
	//	PlaySound(soundName, volume, false, false, delay, endDel, userData);
	//}

	//public void PlaySound(string soundName, float volume, float delay)
	//{
	//	PlaySound(soundName, volume, delay, null, null);
	//}

	//public void PlaySound(string soundName, float delay)
	//{
	//	PlaySound(soundName, 1, delay, null, null);
	//}

	public void StopSound(string soundName)
	{
		for (int i = 0; i < spawnedAudio.Count; ++i)
		{
			var audioData = spawnedAudio[i];
			if (audioData.IsMusic == false
				&& audioData.audioSource != null
				&& audioData.audioSource.clip != null
				&& audioData.audioSource.clip.name.Equals(soundName))
				StopAudio(audioData);
		}

		// Destroy end music
		DespawnEndAudios();
	}

	public bool IsMusicPlaying(string soundName)
	{
		for (int i = 0; i < spawnedAudio.Count; ++i)
		{
			var item = spawnedAudio[i];
			if (item.IsMusic
				&& item.playState != PlayState.FadeOut
				&& item.playState != PlayState.End
				&& item.audioSource.clip.name.Equals(soundName, StringComparison.CurrentCultureIgnoreCase))
				return true;
		}

		return false;
	}

	private void ExecutePlayMusic(AudioClip audioClip, bool loop, float volume, bool ignoreListenerPause = false, bool ignoreListenerVolume = false, float delay = 0, float fadeTime = 0)
	{
		if (audioClip == null)
			return;

		fadeTime = Mathf.Max(0, fadeTime);

		var audioData = SpawnAudio();
		audioData.isMusic = true;
		audioData.audioSource.clip = audioClip;
		audioData.audioSource.ignoreListenerPause = ignoreListenerPause;
		audioData.audioSource.ignoreListenerVolume = ignoreListenerVolume;
		audioData.audioSource.loop = loop;
		audioData.audioSource.spatialBlend = 0;
		audioData.createdFrameCount = Time.frameCount;
		audioData.createdTime = Time.time;
		audioData.volume = MusicVolume;//= volume;
		audioData.delay = delay;
		audioData.fadeTime = fadeTime;

		audioData.UpdateVolume(CurrentMusicVolume);

        if (!NeedPlayMusic)   //播放声音接口太多 先加这 有空这整理一下
        {
            return;
        }
		audioData.audioSource.PlayDelayed(delay);
	}

	//public void PlayMusic(AudioClip audioClip, bool loop, float delay, float fadeTime)
	//{
	//	PlayMusic(audioClip, loop, 1, false, false, delay, fadeTime);
	//}

	public void PlayMusic(string audioName, bool loop, float volume = MUSIC_DEFAULT_VOLUME, bool ignoreListenerPause = false, bool ignoreListenerVolume = false, float delay = 0, float fadeTime = 0)
	{
		// Load sound clip
		AudioClip audioClip = ResourceManager.Instance.LoadAsset<AudioClip>(AssetType.Music, audioName);

		if (audioClip != null)
			audioClip.name = audioName;
		backMusicName = audioName;
		ExecutePlayMusic(audioClip, loop, volume, ignoreListenerPause, ignoreListenerVolume, delay, fadeTime);
	}

	//public void PlayMusic(string audioName, bool loop, float delay, float fadeTime)
	//{
	//	PlayMusic(audioName, loop, 1, false, false, delay, fadeTime);
	//}

	public void PlayMusic(string audioName)
	{
		backMusicName = audioName;
		PlayMusic(audioName, true, 1, false, false, 0, 0);
	}

	public void StopMusic(float fadeTime)
	{
		for (int i = 0; i < spawnedAudio.Count; ++i)
		{
			var audioData = spawnedAudio[i];
            //if (audioData.IsMusic)
				StopMusic(audioData, fadeTime);
		}

		// Destroy end music
		DespawnEndAudios();
	}

	public void CrossFadeMusic(string audioName, bool loop, float fadeTime)
	{
		StopMusic(fadeTime);
		PlayMusic(audioName, loop, 0, fadeTime: fadeTime);
	}

	public void StopMusic()
	{
		StopMusic(0);
	}

	private void StopMusic(AudioData audioData, float fadeTime)
	{
		if (audioData.audioSource == null || audioData.audioSource.clip == null)
			return;

		// Not stated, destroy directly
		if (audioData.playState == PlayState.NotStart)
			StopAudio(audioData);

		if (audioData.playState == PlayState.End)
			return;

		// Skip fading out music
		if (audioData.playState == PlayState.FadeOut)
			return;

		// Playing
		if (fadeTime != 0)
		{
			audioData.volume = audioData.CurrentVolume;
			audioData.playState = PlayState.FadeOut;
			audioData.fadeTime = fadeTime;
			audioData.fadeStartTime = Time.time;
			audioData.fadeVolume = audioData.volume;
		}
		else
		{
			// Stop directly
			StopAudio(audioData);
		}
	}

	public void StepMusicToNormalState(string audioName)
	{
		foreach (var item in spawnedAudio)
			Debug.Log(item.audioSource.clip.name);

		AudioData playingMusic = null;
		foreach (var item in spawnedAudio)
			if (item.IsMusic
				&& item.playState != PlayState.FadeOut
				&& item.playState != PlayState.End
				&& item.audioSource.clip.name.Equals(audioName, StringComparison.CurrentCultureIgnoreCase))
			{
				playingMusic = item;
				break;
			}

		if (playingMusic == null || playingMusic.playState == PlayState.Normal)
			return;

		// Calculate time
		float stepTime = playingMusic.createdTime + playingMusic.delay + playingMusic.fadeTime - Time.time;
		Debug.Assert(stepTime >= 0);
		if (stepTime < 0)
			return;

		// Revert music
		foreach (var item in spawnedAudio)
			if (item.IsMusic)
			{
				item.createdTime -= stepTime;
				if (item.fadeStartTime != 0)
					item.fadeStartTime -= stepTime;
			}
	}

	#region Pool
	private AudioData CreateAudioData()
	{
		var audioData = new AudioData();
		audioData.audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
		audioData.audioSource.playOnAwake = false;
		// DontDestroyOnLoad only work for root GameObjects or components on root GameObjects.
	//	ObjectUtility.AttachToParentAndResetLocalTrans(this.gameObject, audioData.audioSource.gameObject);

		DontDestroyOnLoad(audioData.audioSource.gameObject);

		return audioData;
	}

	private AudioData SpawnAudio()
	{
		AudioData item = null;
		if (despawnedAudio.Count != 0)
		{
			// Use old
			item = despawnedAudio[despawnedAudio.Count - 1];
			despawnedAudio.RemoveAt(despawnedAudio.Count - 1);
		}
		else
		{
			// No cached, create new one
			item = CreateAudioData();
		}

		// Add to spawned list
		spawnedAudio.Add(item);

		// Prepare to play
		item.Reset();

		return item;
	}

	private void DespawnAudio(AudioData audioData)
	{
		// Reset data
		audioData.audioSource.Stop();
		audioData.audioSource.clip = null;
		audioData.audioSource.transform.localPosition = Vector3.zero;
		audioData.playState = PlayState.NotStart;
		audioData.endDel = null;
		audioData.userData = null;

		// Return to pool
		spawnedAudio.Remove(audioData);
		despawnedAudio.Add(audioData);
	}

	private void DespawnEndAudios()
	{
		audioTempList.Clear();

		// Destroy end music
		for (int i = 0; i < spawnedAudio.Count; ++i)
		{
			var audioData = spawnedAudio[i];
			if (audioData.playState == PlayState.End)
				audioTempList.Add(audioData);
		}

		for (int i = 0; i < audioTempList.Count; ++i)
			DespawnAudio(audioTempList[i]);

		audioTempList.Clear();
	}
	#endregion

	public override void OnUpdate()
	{
		// 每帧检查main camera, 把自己办
		if (audioLisnterTransform != null)
		{
			// 若camera 改变，更新缓存transform, 检查删除重复lisnter
			UnityEngine.Camera cam = UnityEngine.Camera.main;
			if(currentAttatchedCamera != cam)
			{
				currentAttatchedCamera = cam;
				if (currentAttatchedCamera != null)
				{
					// upgrade
					currentAttachedCameraTransform = currentAttatchedCamera.transform;

					// del
					var camLisnter = currentAttatchedCamera.GetComponent<AudioListener>();
					if (camLisnter != null)
						UnityEngine.GameObject.Destroy(camLisnter);
				}
			}

			if (currentAttatchedCamera != null)
			{
				audioLisnterTransform.transform.position = currentAttachedCameraTransform.position;
			}
		}
		base.OnUpdate();
        //if (!NeedPlayMusic)
        //    return;

		// Update
		for (int i = 0; i < spawnedAudio.Count; ++i)
		{
			var audioData = spawnedAudio[i];
			if (audioData.IsMusic)
				UpdateMusic(audioData);
			else
				UpdateSound(audioData);
		}

		// Destroy end music
		DespawnEndAudios();
	}

	private void UpdateSound(AudioData audioData)
	{
	    if (!NeedPlaySound)
	        return;
	    // Not started, check to start
		if (audioData.playState == PlayState.NotStart)
		{
			// Update delay
			if (audioData.audioSource.isPlaying == false && Time.time - audioData.createdTime >= audioData.delay)
				audioData.audioSource.Play();

			if (audioData.audioSource.isPlaying)
				audioData.playState = PlayState.Normal;
		}

		// Playing, check end
		if (audioData.playState == PlayState.Normal && audioData.audioSource.isPlaying == false)
			StopAudio(audioData);
	}

	private void UpdateMusic(AudioData audioData)
	{
	    if (!NeedPlayMusic)
	        return;
		// Not started, check to start fade-in
		if (audioData.playState == PlayState.NotStart)
		{
			// Update delay
			if (audioData.audioSource.isPlaying == false && Time.time - audioData.createdTime >= audioData.delay)
				audioData.audioSource.Play();

			if (audioData.audioSource.isPlaying)
			{
				if (audioData.fadeTime != 0)
				{
					// Start fade in
					audioData.playState = PlayState.Fadein;
					audioData.fadeStartTime = audioData.createdTime + audioData.delay;
				}
				else
				{
					audioData.playState = PlayState.Normal;
				}
			}
		}

		// Update fade-in
		if (audioData.playState == PlayState.Fadein)
		{
			// Update volume
			if (Time.time - audioData.fadeStartTime < audioData.fadeTime)
			{
				audioData.fadeVolume = Mathf.Lerp(0, audioData.volume, (Time.time - audioData.fadeStartTime) / audioData.fadeTime);
			}
			else
			{
				// Fade-in finished
				audioData.fadeTime = 0;
				audioData.fadeVolume = audioData.volume;
				audioData.playState = PlayState.Normal;
			}

			// Update volume according to music volume
			audioData.UpdateVolume(CurrentMusicVolume);
		}

		// Update fade-out
		if (audioData.playState == PlayState.FadeOut)
		{
			// Update volume
			if (Time.time - audioData.fadeStartTime < audioData.fadeTime)
			{
				audioData.fadeVolume = Mathf.Lerp(audioData.volume, 0, (Time.time - audioData.fadeStartTime) / audioData.fadeTime);

				// Update volume according to music volume
				audioData.UpdateVolume(CurrentMusicVolume);
			}
			else
			{
				// Fade-in finished
				StopAudio(audioData);
			}
		}
	}

	private void StopAudio(AudioData audioData)
	{
		if (audioData.audioSource != null)
			audioData.audioSource.Stop();

		audioData.playState = PlayState.End; // Mark as end and delete later
		if (audioData.endDel != null)
			audioData.endDel(audioData.userData);
	}

	private void UpdateAudioVolume(bool music)
	{
		foreach (var item in spawnedAudio)
			if (item.IsMusic == music)
				item.UpdateVolume(music ? CurrentMusicVolume : CurrentSoundVolume);

		if (music == false)
			foreach (var kvp in persistentAudios)
				kvp.Value.UpdateVolume(CurrentSoundVolume);
	}

	private AudioData GetBatchingSound(AudioClip audioClip, float delay, int createdframeCount, object userData)
	{
		foreach (var item in spawnedAudio)
			if (item.IsMusic == false
				&& item.audioSource.clip == audioClip
				&& Mathf.Approximately(item.delay, delay)
				&& item.createdFrameCount == createdframeCount
				&& item.userData == userData)
				return item;

		return null;
	}

	private void SoundVolumeChangedDel(float volume)
	{
		SoundVolume = volume;
	}

	private void MusicVolumeChangedDel(float volume)
	{
		MusicVolume = volume;
	}

#if ENABLE_AUDIO_MANAGER_TEST
	void Start()
	{
		Initialize();
		SysModuleManager.Instance.Initialize(gameObject);
		SysModuleManager.Instance.AddSysModule<ResourceManager>(true);
	}

	void Update()
	{
		OnUpdate();
	}

	void OnGUI()
	{
		if (GUILayout.Button("Play sound"))
			PlaySound("ATK_Fire", 0);

		if (GUILayout.Button("Play sound delay"))
			PlaySound("ATK_Light", 1);

		if (GUILayout.Button("Play music"))
			PlayMusic("BGM_MainCity", false, 0, 0);

		if (GUILayout.Button("Play music delay"))
			PlayMusic("BGM_Login", false, 1, 0);

		if (GUILayout.Button("Play music fade"))
			PlayMusic("BGM_Login", false, 0, 10);

		if (GUILayout.Button("Play music delay fade"))
			PlayMusic("BGM_Login", false, 3, 5);

		if (GUILayout.Button("stop music"))
			StopMusic(0);

		if (GUILayout.Button("stop music fade"))
			StopMusic(5);

		if (GUILayout.Button("StepMusicToNormalState"))
			StepMusicToNormalState("BGM_Login");

		SoundMuted = GUILayout.Toggle(SoundMuted, "SoundMuted");
		SoundVolume = GUILayout.HorizontalSlider(SoundVolume, 0, 1);
		MusicMuted = GUILayout.Toggle(MusicMuted, "MusicMuted");
		MusicVolume = GUILayout.HorizontalSlider(MusicVolume, 0, 1);
	}
#endif
}