using UnityEngine;
using LywGames;

public class GameMain : MonoSingleton<GameMain>
{

    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = 30;
        Application.backgroundLoadingPriority = ThreadPriority.BelowNormal;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        DontDestroyOnLoad(this);
    }
    
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        GlobalManager.Instance.Initialize(gameObject);
        GlobalManager.Instance.Add<GameStateMachineManager>().EnterState<GameStateInitializing>();
    }

    private void Update()
    {
        GlobalManager.Instance.Update();
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        GlobalManager.Instance.OnGUI();
    }
#endif

}
