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
        //Debug.Log(GetColorLog("green", "liyangwei", "520-888", "$quxifu"));
    }

    public static string GetColorLog(string color, params string[] msgs)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<color=").Append(color).Append(">");
        for (int i = 0; i < msgs.Length; i++)
        {
            if (msgs[i].StartsWith("$"))
            {
                msgs[i] = string.Format("<color=cyan>{0}</color>", msgs[i].Replace("$", ""));
            }
            System.Text.RegularExpressions.Regex objNotNumberPattern = new System.Text.RegularExpressions.Regex(@"[0-9.-]");
            if (objNotNumberPattern.IsMatch(msgs[i]))
            {
                msgs[i] = string.Format("<color=orange>{0}</color>", msgs[i]);
            }
            sb.Append(msgs[i]).Append(" ");
        }
        sb.Append("</color>");
        return sb.ToString();
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
