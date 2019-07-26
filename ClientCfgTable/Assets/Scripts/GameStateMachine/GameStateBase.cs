using System.Collections;
using System.Reflection;
using UnityEngine;
using Combat;

/// <summary>
/// 游戏状态机基类
/// </summary>
public abstract class GameStateBase : MonoBehaviour
{
    public enum GameStateType
    {
        InValid,
        CentryCity,
        Dungeon,
        CreatePlayerAvatar,
        Initializing,
        Login,
        RetriveGameData,
        SelectArea,
        StoryCampaign,
        SwitchingWorld,
        Upgrading,
        Reconnecting,
        QueryGateServer,
        DownloadConfigInGame
    }

    public abstract bool IsGamingState { get; }
    public abstract GameStateType StateType { get; }

    protected bool isLoading = false;

    private bool isInStack = false; // 此时应停止所有update
    public bool IsInStack { get { return isInStack; } }

    private GameStateMachineManager machineManager;
    public GameStateMachineManager MachineManager
    {
        get { return machineManager; }
        set { machineManager = value; }
    }

    public virtual void Create(object userData) { }
    public virtual void Dispose() { }

    public virtual void PrepareEnter() { }
    public virtual void Enter() { }
    public virtual void DoneEnter() { }

    public virtual void PrepareExit() { }
    public virtual void Exit() { }
    public virtual void DoneExit() { }

    public virtual void OnUpdate() { }
#if UNITY_EDITOR
    public virtual void OnGUIUpdate() { }
#endif

    public virtual void PreparePushStack() { }
    public virtual void PushStack() { isInStack = true; }
    public virtual void DonePushStack() { }

    public virtual void PreparePopStack() { }
    public virtual void PopStack() { isInStack = false; }
    public virtual void DonePopStack() { }

    public virtual bool IsLoading()
    {
        return isLoading;
    }

    public bool IsCombatState()
    {
        return StateType == GameStateType.Dungeon;
    }

    protected void StartLoadingBarUpdateCoroutine(UiPnlLoading loading)
    {
        StopCoroutine("UpdateLoadingBar");
        StartCoroutine(UpdateLoadingBar(loading));
    }

    [Obfuscation(Exclude = true, Feature = "renaming")]
    protected IEnumerator UpdateLoadingBar(UiPnlLoading loading)
    {
        int safeGuard = 0;
        loading.SetSliderValue(0f);
        while (loading.loadingProgress.CurrentAsyncLoadingProgress < 1f)
        {
            loading.SetSliderValue(loading.loadingProgress.CurrentAsyncLoadingProgress);
            safeGuard++;
            if (safeGuard >= 25 * 1000 / 33)
            {
                break;
            }
            yield return null;
        }
        loading.SetSliderValue(1f);
        yield return null; 
    }

    protected ObjectBase CreateObject(int templateId, int idForGenerateName, bool clientNotServer, int createionMethod = ObjectCreationContext.CreationMethodType.OBJECT_CREATION_METHOD)
    {
        ObjectCreationContext context = new ObjectCreationContext();
        context.templateId = templateId;
        context.clientNotServer = clientNotServer;
        context.CreationMethod = createionMethod;
        context.NetworkID = idForGenerateName;

        return InstanceBase.GetCurrentInstance().GetObjectManager().CreateObject(context);
    }

}
