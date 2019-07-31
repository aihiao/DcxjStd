using UnityEngine;
using LywGames;
using LywGames.Effect;

/// <summary>
/// ����Gameplay���Plugin��Ĵ���
/// �����ʵ����һЩGameplay�ķ�����ͨ���ӿڹ�Plugin�����
/// </summary>
public class GameplayFunCaller : AbsManager<GameplayFunCaller>, IGameplayFunctionCall
{
	public override void Initialize(params object[] parameters)
	{
		FXController.FxStaticGameplayCaller = this;
		AnimationEventHandler.AnimEntStaticGameplayCaller = this;
	}

	public bool IsConfigDataBaseAvailable()
	{
		return ClientCommon.ConfigDataBase.Instance != null;
	}
	public void PoolManagerStore(GameObject obj)
	{
		PoolManager.Instance.Store(obj);
	}

	public void PoolManagerStorePfxType(GameObject obj/*, string name*/)
	{
		PoolManager.Instance.Store(obj, ClientCommon.AssetType.PFX.ToString());
	}

	public bool IsPoolManagerInEditorMode()
	{
		return PoolManager.Instance.IsInEditorMode;
	}

	public float GetAudioRolloffMinDistance()
	{
		return ClientCommon.ConstValue.AudioRolloffMinDistance;
	}
	public float GetAudioRolloffMaxDistance()
	{
		return ClientCommon.ConstValue.AudioRolloffMaxDistance;
	}

	//if (KodGames.Macro.AssetTrue(AudioManager.Instance == null && GameStateMachineManager.Instance != null))
	//	return;

	//if (!AudioManager.Instance.NeedPlaySound)
	//	return;
	public bool SuitForPlaySound()
	{
        if (AssertHelper.AssetTrue(AudioManager.Instance == null && GameStateMachineManager.Instance != null))
			return false;

		if (!AudioManager.Instance.NeedPlaySound)
			return false;

		return true;
	}


	public void Play3DSound(string audioName, Vector3 worldPosition, float volume, float delay, float minDistance = 1, float maxDistance = 500)
	{
		AudioManager.Instance.Play3DSound(audioName, worldPosition, volume, delay, minDistance: minDistance, maxDistance: maxDistance);
	}
	public void PlaySound(string audioName, float volume, bool ignoreListenerPause = false, bool ignoreListenerVolume = false,
						  float delay = 0)
	{
		AudioManager.Instance.PlaySound(audioName, volume: volume, delay: delay);
	}

	public bool IsMusicPlaying(string audioName)
	{
		return AudioManager.Instance.IsMusicPlaying(audioName);
	}

	public void PlayMusic(string audioName, bool loop, float volume, bool ignoreListenerPause = false, bool ignoreListenerVolume = false, float delay = 0, float fadeTime = 0)
	{
		AudioManager.Instance.PlayMusic(audioName, true, delay, fadeTime: fadeTime);
	}

	public void StopMusic(float fadeTime)
	{
		AudioManager.Instance.StopMusic(fadeTime);
	}

	public void StepMusicToNormalState(string audioName)
	{
		AudioManager.Instance.StepMusicToNormalState(audioName);
	}

	//		if (animEvent.functionName == ClientCommon.AnimationEvtHandlerFunc.GetNameByType(ClientCommon.AnimationEvtHandlerFunc.MarkKeyFrame)
	//|| animEvent.functionName == ClientCommon.AnimationEvtHandlerFunc.GetNameByType(ClientCommon.AnimationEvtHandlerFunc.MarkLogicFrame))
	public string GetMarkKeyFrameFunName()
	{
		return ClientCommon.AnimationEvtHandlerFunc.GetNameByType(ClientCommon.AnimationEvtHandlerFunc.MarkKeyFrame);
	}



	public string GetMarkLogicFrameFunName()
	{
		return ClientCommon.AnimationEvtHandlerFunc.GetNameByType(ClientCommon.AnimationEvtHandlerFunc.MarkLogicFrame);
	}

	//  ��AnimationEventHandler�Ѵ���Ų��������������ĵ���̫�����ϲ�

	[System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
	public void PlayFx(string parameter)
	{
		string effectName, parentGameObjectName;
		AnimationEventHandler.PlayFxParameter.Parse(parameter, out effectName, out parentGameObjectName);

		Transform parentGO = this.gameObject.transform;
		if (string.IsNullOrEmpty(parentGameObjectName) == false)
			parentGO = ObjectUtility.FindChildObject(parentGO, parentGameObjectName);
		FxManager.Instance.PlayFX(effectName, parentGO.gameObject, false, true, true, true);

		Debug.LogError("������Ҫ�ģ�����õĻ�");
	}


	//  ��AnimationEventHandler�Ѵ���Ų��������������ĵ���̫�����ϲ�
	[System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
	public void PlayAbilityPfx(string xmlParameter, GameObject pfxParentObj)
	{
		Combat.ParticleSystemSettings settings = Combat.ParticleSystemSettings.Load(xmlParameter);

		var pfx = FxManager.Instance.PlayFX(settings.particleName, pfxParentObj, true, false, false, false);
		if (pfx == null)
			return;

		if (settings.maintainPosition && settings.maintainFacing)
			pfx.Root.parent = null;

		//����
		pfx.Root.parent = null;

		// ���������ӣ�������Ҳû�ã������ı����λ��
		//pfx.Root.localScale = new Vector3(
		//pfxParentObj.transform.localScale.x == 0 ? 0 : 1 / pfxParentObj.transform.lossyScale.x,
		//pfxParentObj.transform.localScale.y == 0 ? 0 : 1 / pfxParentObj.transform.lossyScale.y,
		//pfxParentObj.transform.localScale.z == 0 ? 0 : 1 / pfxParentObj.transform.lossyScale.z);

		pfx.Root.localScale = Vector3.one;
		pfx.Root.parent = pfxParentObj.transform;

		//����
		pfx.Root.forward = pfxParentObj.transform.forward;
		pfx.Root.localPosition = Vector3.zero;
		pfx.Root.localPosition += settings.placementLocalRelativePosition;
		pfx.Root.localPosition += (UnityEngine.Camera.main.transform.position - pfx.Root.position).normalized * settings.attachmentZOffset;

		//pfx.Root.localEulerAngles += settings.placementRelativeEuler;
		if (settings.placementRelativeEuler.IsEqual(Vector3.zero) == false)
			pfx.Root.localEulerAngles = settings.placementRelativeEuler;

		var pc = pfx.GetComponent<Combat.ParticleAbilityParticleController>();
		if (pc == null)
			pc = pfx.gameObject.AddComponent<Combat.ParticleAbilityParticleController>();

		pc.Set(pfx.Root.gameObject, settings, false);

		FxManager.Instance.StartFx(pfx);
	}

	[System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
	public void ShakeMainCamera(string parameter)
	{
		if (Camera.main == null)
			return;

		float intensity, duration, interval;
		bool shakeOnlyVisible;
		AnimationEventHandler.ShakeMainCameraParameter.Parse(parameter, out intensity, out duration, out interval, out shakeOnlyVisible);
		CameraShaker.Shake(Camera.main.gameObject, intensity, duration, interval, shakeOnlyVisible: shakeOnlyVisible, sourceObject: this.gameObject);
	}


	public int GetGloableFxPropertyLevel()
	{
		return GameConfigManager.GloableFxPropertyLevel;
	}
}