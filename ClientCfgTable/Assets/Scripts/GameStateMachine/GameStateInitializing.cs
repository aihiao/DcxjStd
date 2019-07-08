using ClientCommon;

/// <summary>
/// 游戏进入的第一个状态, 这个状态在游戏整个生命周期只会进入一次
/// </summary>
public class GameStateInitializing : GameStateBase
{
    bool isInited = false;

    public override bool IsGamingState { get { return false; } }

    public override GameStateType StateType { get { return GameStateType.Initializing; } }

    public override void Enter()
    {
        GlobalManager.Instance.Add<GameShellManager>();

        StartCoroutine(ConfigDataBase.Instance.ConstructLocalConfigDbFile(Defines.ConfigsFolder, this));
    }

    void Initialize()
    {
        if (isInited)
        {
            return;
        }
        isInited = true;

        ConfigDataBase.Instance.Initialize(new SqliteAccessorFactory(), Defines.ConfigsFolder);
    }

    void Update()
    {
        if (ConfigDataBase.Instance.ConstructedLocalDbError) // 解压数据库库文件完成，但是出错
        {
            return;
        }

        if (!ConfigDataBase.Instance.ConstructedLocalDb)
        {
            return;
        }

        if (!isInited)
        {
            Initialize();
        }

        GameStateMachineManager.Instance.EnterState<GameStateQueryGateServer>();
    }

}
