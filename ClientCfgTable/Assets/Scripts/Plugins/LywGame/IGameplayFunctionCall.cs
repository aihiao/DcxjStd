using UnityEngine;

/// <summary>
/// 需要把Plugin 从游戏逻辑层剥离出来，使得游戏逻辑依赖Plugin，并且Plugin不依赖于游戏逻辑
/// 但是，在fxController等类的实现代码里，有很多需要访问游戏逻辑层的代码， 比如访问audioManager，所以定义此接口，运行时把接口的实例传过来
/// </summary>
public interface IGameplayFunctionCall
{
	bool IsConfigDataBaseAvailable();
	void PoolManagerStore(GameObject obj);

	void PoolManagerStorePfxType(GameObject obj/*, string name*/);

	bool IsPoolManagerInEditorMode();

	float GetAudioRolloffMinDistance();
	float GetAudioRolloffMaxDistance();

	bool SuitForPlaySound();


	void Play3DSound(string audioName, Vector3 worldPosition, float volume, float delay, float minDistance = 1, float maxDistance = 500);
	void PlaySound(string audioName, float volume, bool ignoreListenerPause = false, bool ignoreListenerVolume = false,
						  float delay = 0);

	bool IsMusicPlaying(string audioName);

	void PlayMusic(string audioName, bool loop, float volume, bool ignoreListenerPause = false, bool ignoreListenerVolume = false, float delay = 0, float fadeTime = 0);

	void StopMusic(float fadeTime);

	void StepMusicToNormalState(string audioName);

	string GetMarkKeyFrameFunName();
	string GetMarkLogicFrameFunName();

	void PlayFx(string parameter);
	void PlayAbilityPfx(string xmlParameter, UnityEngine.GameObject parent);
	
	void ShakeMainCamera(string parameter);

	int GetGloableFxPropertyLevel();
}
