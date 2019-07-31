using System;
using System.Collections.Generic;
using UnityEngine;
using LywGames.Effect;

public class FXController : MonoBehaviour
{
#if UNITY_EDITOR
    public static int acitvedFxCount = 0;
#endif

    public static IGameplayFunctionCall FxStaticGameplayCaller = null;

    [Serializable]
    public abstract class FXData
    {
        public float beginTime;
        public float endTime;
        protected bool played = false;

        [HideInInspector]
        public FXController fxController = null;

        public abstract bool IsDead { get; }
        public abstract void Play();
        public abstract void Stop();
        public abstract void Reset(bool clearFX);
        public virtual void FadeOut(float remainTime) { }

        public virtual void Update(float playingTime)
        {
            if (playingTime >= beginTime)
            {
                if (playingTime <= endTime)
                {
                    if (played == false)
                        Play();
                }
                // End time smaller than zero means need not stop
                else if (endTime >= 0)
                {
                    if (played)
                        Stop();
                }
            }
        }

    }

    [Serializable]
    public class ParticleData : FXData
    {
        public ParticleEmitter emitter;

        public ParticleData Create(ParticleEmitter emitter)
        {
            this.emitter = emitter;
            this.beginTime = 0;
            this.endTime = -1;
            return this;
        }

        public override bool IsDead
        {
            get { return emitter.particleCount == 0; }
        }

        public override void Play()
        {
            played = true;
            emitter.emit = true;
        }

        public override void Stop()
        {
            emitter.emit = false;
        }

        public override void Reset(bool clearFX)
        {
            played = false;
            emitter.emit = false;
            if (clearFX)
                emitter.ClearParticles();
        }

        public override void FadeOut(float remainTime)
        {
            if (emitter.particleCount == 0)
                return;

            // Extract the particles, here we must create new particleSystem array to update the effect according to the unity notes.
            Particle[] particleDatas = emitter.particles;
            Particle[] newParticleDatas = new Particle[particleDatas.Length];

            for (int j = 0; j < particleDatas.Length; j++)
            {
                newParticleDatas[j] = new Particle();
                newParticleDatas[j] = particleDatas[j];

                if (newParticleDatas[j].energy > remainTime)
                    newParticleDatas[j].energy = remainTime;
            }

            emitter.particles = newParticleDatas;
        }
    }

    [Serializable]
    public class ParticleSystemData : FXData
    {
        public ParticleSystem particleSystem;

        public int PropertyLevel = 1;

        public ParticleSystemData Create(ParticleSystem particleSystem)
        {
            particleSystem.playOnAwake = false;

            this.particleSystem = particleSystem;
            this.beginTime = 0;
            this.endTime = -1;
            return this;
        }

        public override bool IsDead
        {
            get { return particleSystem.particleCount == 0; }
        }

        public override void Play()
        {
            played = true;
            particleSystem.Play();
        }

        public override void Stop()
        {
            particleSystem.Stop();
        }

        public override void Reset(bool clearFX)
        {
            played = false;
            particleSystem.Stop();
            particleSystem.playOnAwake = false;
        }

        public override void FadeOut(float remainTime)
        {
            if (particleSystem.particleCount == 0)
                return;

            // Extract the particleDatas, here we must create new particleSystem array to update the effect according to the unity notes.
            ParticleSystem.Particle[] particleDatas = new ParticleSystem.Particle[particleSystem.particleCount];
            particleSystem.GetParticles(particleDatas);
            ParticleSystem.Particle[] newParticleDatas = new ParticleSystem.Particle[particleDatas.Length];

            for (int j = 0; j < particleDatas.Length; j++)
            {
                newParticleDatas[j] = new ParticleSystem.Particle();
                newParticleDatas[j] = particleDatas[j];

                if (newParticleDatas[j].lifetime > remainTime)
                    newParticleDatas[j].lifetime = remainTime;
            }

            particleSystem.SetParticles(newParticleDatas, newParticleDatas.Length);
        }
    }

    [Serializable]
    public class AnimationData : FXData
    {
        public Animation animation;

        public AnimationData Create(Animation animation)
        {
            this.animation = animation;
            this.beginTime = 0;
            this.endTime = -1;
            return this;
        }

        public override bool IsDead
        {
            get { return animation.isPlaying == false; }
        }

        public override void Play()
        {
            played = true;
            animation.Play();
        }

        public override void Stop()
        {
            animation.Stop();
        }

        public override void Reset(bool clearFX)
        {
            played = false;
            animation.playAutomatically = false;
        }
    }

    [Serializable]
    public class TrailRendererData : FXData
    {
        public TrailRenderer renderer;

        public TrailRendererData Create(TrailRenderer renderer)
        {
            this.renderer = renderer;
            this.beginTime = 0;
            this.endTime = -1;
            return this;
        }

        public override bool IsDead
        {
            get { return renderer.enabled; }
        }

        public override void Play()
        {
            played = true;
            renderer.enabled = true;
        }

        public override void Stop()
        {
            renderer.enabled = false;
        }

        public override void Reset(bool clearFX)
        {
            played = false;
            renderer.enabled = false;
        }
    }

    [System.Serializable]
    public class SoundFxData : FXData
    {
        public enum SoundType
        {
            _2D,
            _3D
        }

        public string audioAssetName = string.Empty;
        public string audioAssetMetaId = string.Empty;

        public float delayTime = 0f;
        [Range(0, 1)]
        public float volume = 1f;
        public SoundType soundType = SoundType._3D;

        public float minDistance = 20;
        public float maxDistance = 40;
        public bool useMinDistanceConfig = true;
        public bool useMaxDistanceConfig = true;

        public override bool IsDead { get { return true; } }

        public override void Play()
        {
            float minDist = minDistance, maxDist = maxDistance;
            if (useMinDistanceConfig && FxStaticGameplayCaller.IsConfigDataBaseAvailable())
                minDist = FxStaticGameplayCaller.GetAudioRolloffMinDistance();

            if (useMaxDistanceConfig && FxStaticGameplayCaller.IsConfigDataBaseAvailable())
                maxDist = FxStaticGameplayCaller.GetAudioRolloffMaxDistance();

            played = true;

            if (!FxStaticGameplayCaller.SuitForPlaySound())
                return;

            if (soundType == SoundType._3D)
                FxStaticGameplayCaller.Play3DSound(audioAssetName, fxController.transform.position, volume, delayTime, minDistance: minDist, maxDistance: maxDist);
            else
                FxStaticGameplayCaller.PlaySound(audioAssetName, volume: volume, delay: delayTime);

        }

        public override void Reset(bool clearFX) { played = false; }

        public override void Stop() { }
    }

    [Serializable]
    public class CameraFxData : FXData
    {
        public float intensity = 1;
        public float duration = 0.3f;
        public float interval = 0.1f;
        public float delay = 0;
        public bool shakeOnlyVisible = true;

        public override bool IsDead { get { return true; } }

        public override void Play()
        {
            played = true;
            CameraShaker.Shake(Camera.main.gameObject, intensity, duration, interval, delay, shakeOnlyVisible, fxController.gameObject);
        }

        public override void Reset(bool clearFX) { played = false; }

        public override void Stop() { }
    }

    public ParticleData[] particleDataArray;
    public ParticleSystemData[] particleSystemDataArray;
    public AnimationData[] animationDataArray;
    public TrailRendererData[] trailRendererDataArray;
    public CameraFxData[] cameraFxDataArray;
    [HideInInspector]
    public SoundFxData[] soundFxDataArray;
    public float life = 1;
    public float fadeDelay = 0f; // Only use to set in editor.
    public bool loop = true;
    public bool autoDestroy = true;

    public enum _DESTROY_MODE
    {
        FADE,
        PARTICLE_DISAPPEAR,
    }

    public _DESTROY_MODE destroyMode = _DESTROY_MODE.FADE;

    private float playingTime;
    private float fadingRemainTime;
    private float lastFreeToPoolTime = 0;

    enum _STAGE
    {
        STOPPED,
        PLAYING,
        STOPPING,
    };

    private _STAGE playingStage = _STAGE.STOPPED;

    // End FX delegate	
    public delegate void OnFXFinishDelegate(object userData);

    private struct DelegateData
    {
        public OnFXFinishDelegate del;
        public object userData;

        public DelegateData(OnFXFinishDelegate del, object userData)
        {
            this.del = del;
            this.userData = userData;
        }
    }
    private List<DelegateData> onFXFinishDels;

    private Transform root;
    public Transform Root
    {
        get { return root != null ? root : this.transform; }
    }

    float movingTime;
    float totalMoveTime;
    Vector3 moveStartPos;
    Vector3 moveDestPos;
    Action OnMoveCompleted = null;

    public void CreateRoot()
    {
        if (root == null)
        {
            root = new GameObject(this.gameObject.name).transform;
            ObjectUtility.AttachToParentAndKeepLocalTrans(root, this.transform);

            // Reset root transform
            root.localPosition = Vector3.zero;
            root.localRotation = Quaternion.identity;
            root.localScale = Vector3.one;
        }
    }

    public void SetSortOrder(int sortOrder)
    {
        DoSetSortOrderForChildren(this.transform, sortOrder);
    }

    void DoSetSortOrderForChildren(Transform trans, int sortOrder)
    {
        var renderCmp = trans.GetComponent<Renderer>();
        if (renderCmp != null)
        {
            renderCmp.sortingOrder = sortOrder;
        }

        for (int i = 0; i < trans.childCount; i++)
        {
            DoSetSortOrderForChildren(trans.GetChild(i), sortOrder);
        }
    }

    [ContextMenu("Play FX")]
    public void PlayFX()
    {
        if (playingStage == _STAGE.PLAYING)
            return;

        playingStage = _STAGE.PLAYING;
        playingTime = 0;

        // Particles
        foreach (var data in particleDataArray)
        {
            data.Reset(true);
            if (data.beginTime == 0)
                data.Play();
        }

        // Animations
        foreach (var data in particleSystemDataArray)
        {
            data.Reset(true);
            if (data.beginTime == 0)
                data.Play();
        }

        // Animations
        foreach (var data in animationDataArray)
        {
            data.Reset(true);
            if (data.beginTime == 0)
                data.Play();
        }

        foreach (var data in trailRendererDataArray)
        {
            data.Reset(true);
            if (data.beginTime == 0)
                data.Play();
        }

        //判断是否需要播放音效  如果不开音效 就不需要进行这些操作  提高一点效率
        //通过GameStateMachineManager.Instance判断是否在游戏中。美术单纯看特效时一般不在游戏中，没有初始化 AudioManager
        //if (GameStateMachineManager.Instance != null && AudioManager.Instance.NeedPlaySound)
        if (FxStaticGameplayCaller != null && FxStaticGameplayCaller.SuitForPlaySound())
        {
            foreach (var data in soundFxDataArray)
            {
                data.Reset(true);
                data.fxController = this;
                if (data.beginTime == 0)
                    data.Play();
            }
        }

        foreach (var data in cameraFxDataArray)
        {
            data.Reset(true);
            data.fxController = this;
            if (data.beginTime == 0)
                data.Play();
        }

        //Debug.Log( "Play FXController: name=" + gameObject.name );
    }

    [ContextMenu("Stop FX")]
    public void StopFX()
    {
        // Pfx is stopped.
        if (playingStage != _STAGE.PLAYING)
            return;

        playingStage = _STAGE.STOPPING;
        fadingRemainTime = fadeDelay;

        StopFXData();

        //Debug.Log( "Stop FXController: name=" + gameObject.name );
    }

    private void DestroyFX()
    {
        //if (UsePoolManagerAsPool)
        //{
        //	//	PoolManager.Instance.Store(this.Root.gameObject);
        //	FxStaticGameplayCaller.PoolManagerStore(this.Root.gameObject);
        //}
        //else
        {
            // reset 
            ClearFinishCallback();
            SetFreeToLastPoolTime(Time.time);

            if (FxStaticGameplayCaller != null)
            {
                if (FxStaticGameplayCaller.IsPoolManagerInEditorMode())
                    Destroy(this.Root.gameObject);
                else
                    FxStaticGameplayCaller.PoolManagerStorePfxType(this.Root.gameObject/*, ClientCommon.AssetType.PFX.ToString()*/);
            }
        }
    }

    public void DestroySelf()
    {
        DestroyFX();
    }

    private void StopFXData()
    {
        foreach (var data in particleDataArray)
            data.Stop();

        foreach (var data in particleSystemDataArray)
            data.Stop();

        foreach (var data in animationDataArray)
            data.Stop();

        foreach (var data in trailRendererDataArray)
            data.Stop();

        foreach (var data in soundFxDataArray)
            data.Stop();

        foreach (var data in cameraFxDataArray)
            data.Stop();
    }

    public void ResetFX(bool clearFX)
    {
        foreach (var data in particleDataArray)
            data.Reset(clearFX);

        foreach (var data in particleSystemDataArray)
            data.Reset(clearFX);

        foreach (var data in animationDataArray)
            data.Reset(clearFX);

        foreach (var data in trailRendererDataArray)
            data.Reset(clearFX);

        foreach (var data in soundFxDataArray)
            data.Reset(clearFX);

        foreach (var data in cameraFxDataArray)
            data.Reset(clearFX);

        this.movingTime = 0f;
        this.totalMoveTime = 0f;
    }

    private void UpdateFX()
    {
        foreach (var data in particleDataArray)
            data.Update(playingTime);

        foreach (var data in particleSystemDataArray)
            data.Update(playingTime);

        foreach (var data in animationDataArray)
            data.Update(playingTime);

        foreach (var data in trailRendererDataArray)
            data.Update(playingTime);

        foreach (var data in soundFxDataArray)
            data.Update(playingTime);

        foreach (var data in cameraFxDataArray)
            data.Update(playingTime);
    }

    [ContextMenu("Reset ParticleData Array")]
    public void ResetParticleArray()
    {
        // We can't place into Awake method. Maybe data has not been initialized.
        ParticleEmitter[] pfxEmits = gameObject.GetComponentsInChildren<ParticleEmitter>();
        particleDataArray = new ParticleData[pfxEmits.Length];

        for (int i = 0; i < pfxEmits.Length; i++)
        {
            particleDataArray[i] = new ParticleData().Create(pfxEmits[i]);

            // Turn off the auto destroy caused by the data.
            ParticleAnimator particleAnimator = (ParticleAnimator)pfxEmits[i].gameObject.GetComponentInChildren(typeof(ParticleAnimator));
            if (particleAnimator != null)
                particleAnimator.autodestruct = false;
        }
    }

    [ContextMenu("Reset ParticleData System Array")]
    public void ResetParticleSystemArray()
    {
        // We can't place into Awake method. Maybe data data has not been initialized.
        ParticleSystem[] pfxSystems = gameObject.GetComponentsInChildren<ParticleSystem>();
        particleSystemDataArray = new ParticleSystemData[pfxSystems.Length];

        for (int i = 0; i < pfxSystems.Length; i++)
        {
            particleSystemDataArray[i] = new ParticleSystemData().Create(pfxSystems[i]);

            //// Turn off the auto destroy caused by the data.
            //ParticleAnimator particleAnimator = (ParticleAnimator)data.gameObject.GetComponentInChildren(typeof(ParticleAnimator));
            //if (particleAnimator != null)
            //    particleAnimator.autodestruct = false;
        }
    }

    [ContextMenu("Reset AnimationData Array")]
    public void ResetAnimationArray()
    {
        // Get animations data
        Animation[] animations = gameObject.GetComponentsInChildren<Animation>();
        animationDataArray = new AnimationData[animations.Length];

        for (int i = 0; i < animations.Length; ++i)
            animationDataArray[i] = new AnimationData().Create(animations[i]);
    }

    [ContextMenu("Reset TrailRenderer Array")]
    public void ResetTrailRendererArray()
    {
        // Get trailRenderer data
        TrailRenderer[] trailRenderer = gameObject.GetComponentsInChildren<TrailRenderer>();
        trailRendererDataArray = new TrailRendererData[trailRenderer.Length];

        for (int i = 0; i < trailRenderer.Length; ++i)
            trailRendererDataArray[i] = new TrailRendererData().Create(trailRenderer[i]);
    }

    [ContextMenu("Reset SoundFxArray")]
    public void ResetSoundFxArray()
    {
        if (soundFxDataArray == null)
            soundFxDataArray = new SoundFxData[0];
    }

    [ContextMenu("Reset(Clear) SoundFxArray")]
    public void ResetCameraFxArray()
    {
        if (cameraFxDataArray == null)
            cameraFxDataArray = new CameraFxData[0];
    }

    [ContextMenu("Disable AutoPlay")]
    public void DisableAutoPlay()
    {
        ResetFX(true);
    }

    public void AddFinishCallback(OnFXFinishDelegate finishFun, object userData)
    {
        if (onFXFinishDels == null)
            onFXFinishDels = new List<DelegateData>();

        onFXFinishDels.Add(new DelegateData(finishFun, userData));
    }

    public void ClearFinishCallback()
    {
        if (onFXFinishDels != null)
            onFXFinishDels.Clear();
    }

    [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
    // Use this for initialization
    public void Start()
    {
        if (fadeDelay <= 0)
            fadeDelay = 0;

        // If no FX data, get this from game object.
        if ((particleDataArray == null || particleDataArray.Length == 0) &&
            (particleSystemDataArray == null || particleSystemDataArray.Length == 0) &&
            (animationDataArray == null || animationDataArray.Length == 0) &&
            (trailRendererDataArray == null || trailRendererDataArray.Length == 0))
        //这里没有考虑SoundFxDataArray
        {
            if ((particleDataArray == null || particleDataArray.Length == 0))
                ResetParticleArray();

            if ((particleSystemDataArray == null || particleSystemDataArray.Length == 0))
                ResetParticleSystemArray();

            if ((animationDataArray == null || animationDataArray.Length == 0))
                ResetAnimationArray();

            if ((trailRendererDataArray == null || trailRendererDataArray.Length == 0))
                ResetTrailRendererArray();
        }

        if (soundFxDataArray == null)
            ResetSoundFxArray();

        if (cameraFxDataArray == null)
            ResetCameraFxArray();

        PlayFX();
    }

    public void SetFreeToLastPoolTime(float time)
    {
        this.lastFreeToPoolTime = time;
    }

    public float GetNextPoolTime()
    {
        return lastFreeToPoolTime + GetDelayPoolTime();
    }

    private float GetDelayPoolTime()
    {
        float time = 0;
        foreach (var data in trailRendererDataArray)
            time = Mathf.Max(time, data.renderer.time);

        return time;
    }


    [System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
    // Update is called once per frame
    private void LateUpdate()
    {
        // Pfx is stopped.
        if (playingStage == _STAGE.STOPPED)
            return;

        // Update FX stage.
        if (playingStage == _STAGE.PLAYING)
        {
            // Update FX played time.
            playingTime += Time.deltaTime;

            UpdateMovement();
            UpdateFX();

            if (playingTime > life)
            {
                if (loop)
                {
                    // life > 0 means this FX has recycle duration
                    if (life > 0)
                    {
                        StopFXData();
                        ResetFX(false);
                        playingTime -= life;
                    }
                }
                else
                {
                    StopFX();
                }
            }
        }
        else if (playingStage == _STAGE.STOPPING)
        {
            // Update fading time.
            fadingRemainTime -= Time.deltaTime;

            // Life is end. 
            if (destroyMode == _DESTROY_MODE.FADE)
            {
                FadeOut(fadingRemainTime);

                if (fadingRemainTime <= 0)
                {
                    // Mark flag.
                    playingStage = _STAGE.STOPPED;
                }
            }
            else if (destroyMode == _DESTROY_MODE.PARTICLE_DISAPPEAR)
            {
                if (IsDead())
                {
                    // Mark flag.
                    playingStage = _STAGE.STOPPED;
                }
            }
            else // Error process.
            {
                // Mark flag.
                playingStage = _STAGE.STOPPED;
            }

            // Process stopped stage.
            if (playingStage == _STAGE.STOPPED)
            {
                playingTime = 0;

                // Call back.
                if (onFXFinishDels != null)
                    foreach (var delData in onFXFinishDels)
                        if (delData.del != null)
                            delData.del(delData.userData);

                // Destroy self.
                if (autoDestroy)
                    DestroyFX();
            }
        }

        UpdateFxScale();
    }

    private bool IsDead()
    {
        foreach (var data in particleDataArray)
            if (data.IsDead == false)
                return false;

        foreach (var data in particleSystemDataArray)
            if (data.IsDead == false)
                return false;

        foreach (var data in animationDataArray)
            if (data.IsDead == false)
                return false;

        foreach (var data in trailRendererDataArray)
            if (data.IsDead == false)
                return false;

        //这里没有考虑SoundFxDataArray

        return true;
    }

    private void FadeOut(float remainTime)
    {
        foreach (var data in particleDataArray)
            data.FadeOut(remainTime);

        foreach (var data in particleSystemDataArray)
            data.FadeOut(remainTime);

        foreach (var data in animationDataArray)
            data.FadeOut(remainTime);

        foreach (var data in trailRendererDataArray)
            data.FadeOut(remainTime);

        //这里没有考虑SoundFxDataArray
    }


    public void MoveAndDestroyWhenReach(Vector3 startPos, Vector3 destPos, float totalTime, System.Action onMoveCompleted = null)
    {
        this.moveStartPos = startPos;
        this.moveDestPos = destPos;
        this.totalMoveTime = totalTime;
        this.OnMoveCompleted = onMoveCompleted;
    }

    private void UpdateMovement()
    {
        if (totalMoveTime > 0f)
        {
            movingTime += Time.deltaTime;

            this.transform.position = Vector3.Lerp(moveStartPos, moveDestPos, movingTime / totalMoveTime);
            if (movingTime >= totalMoveTime)
                StopFX();

            if (OnMoveCompleted != null)
                OnMoveCompleted();
            OnMoveCompleted = null;
        }
    }


    bool hasInactived = false;
    void OnEnable()
    {
#if UNITY_EDITOR
        acitvedFxCount++;

        // 编辑技能时忽略此项 = =
        if (FxStaticGameplayCaller == null)
            return;
#endif

        for (int i = 0; i < this.transform.childCount && particleSystemDataArray != null && i < particleSystemDataArray.Length; i++)
        {
            if (FxStaticGameplayCaller != null && particleSystemDataArray[i].PropertyLevel > FxStaticGameplayCaller.GetGloableFxPropertyLevel())
                transform.GetChild(i).gameObject.SetActive(false);
            else
            {
                transform.GetChild(i).gameObject.SetActive(true);

                if (hasInactived)
                {
                    // 尝试解决主城装备问题
                    if (particleSystemDataArray[i].beginTime == 0)
                        particleSystemDataArray[i].Play();
                }
            }

        }

        hasInactived = false;
    }

    void OnDisable()
    {
#if UNITY_EDITOR
        acitvedFxCount--;
#endif
        hasInactived = true;
    }

    void UpdateFxScale()
    {

        //BindMaterials(gameObject, false);
        //if (Camera.current != null)
        //{
        //	for (int i = 0; i < effectToMaterials.Count; i++)
        //	{
        //		//	effectToMaterials[i].SetVector("_ParticleCenter", Camera.current.worldToCameraMatrix.MultiplyPoint(transform.root.position));
        //		if (effectToMaterials[i] != null)
        //			effectToMaterials[i].SetVector("_ParticleScaling", enableFxScale ? transform.lossyScale : Vector3.one);
        //	}
        //}


    }

}
