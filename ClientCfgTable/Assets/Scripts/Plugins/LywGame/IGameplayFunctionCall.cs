using UnityEngine;

/// <summary>
/// ��Ҫ��Plugin ����Ϸ�߼�����������ʹ����Ϸ�߼�����Plugin������Plugin����������Ϸ�߼�
/// ���ǣ���fxController�����ʵ�ִ�����кܶ���Ҫ������Ϸ�߼���Ĵ��룬 �������audioManager�����Զ���˽ӿڣ�����ʱ�ѽӿڵ�ʵ��������
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
