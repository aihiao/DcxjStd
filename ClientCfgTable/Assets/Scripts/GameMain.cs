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

        DontDestroyOnLoad(gameObject);
    }
    
    private void Start()
    {
        DontDestroyOnLoad(this);
        Initialize();
    }

    private void Initialize()
    {
        TimeEx.Initialize();

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

    public void CreatePlatform()
    {
        if (GetComponent<Platform>() != null)
        {
            return;
        }
        IPlatformListener platformListener = PlatformListener.Instance;
        Platform.CreateOnGameObject(gameObject, platformListener);
    }

}
