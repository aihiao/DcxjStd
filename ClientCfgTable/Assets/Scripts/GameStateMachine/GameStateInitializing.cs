using UnityEngine;
using ClientCommon;
using LywGames;

/// <summary>
/// 游戏进入的第一个状态, 这个状态在游戏整个生命周期只会进入一次
/// </summary>
public class GameStateInitializing : GameStateBase
{
    bool isInited = false;
    bool hasInitedAssetbundles = false;
    bool unpackedAssetbundles = false;
    bool initFaildPanelIsShown = false;

    public override bool IsGamingState { get { return false; } }

    public override GameStateType StateType { get { return GameStateType.Initializing; } }

    public override void Enter()
    {
        /*
		 * 显示初始化的提示面板，因为还没有初始化，没法用UIManager。
		 * 界面直接挂在了场景中，因为没有初始化，没有UIRoot。1/320是因为做的界面都默认放大了320倍。。
		 * （默认放到UIRoot下就会这样，即使改过来以后再修改界面的时候也默认设置过来，所以还是这里处理下吧）
		 */
        Object prefab = Resources.Load<Object>(@"objects/ui/common_ngui/uipnlinitiation");
        GameObject panel = Instantiate(prefab) as GameObject;
        panel.transform.localPosition = Vector3.zero;
        float a = 1, b = 320, c = a / b;
        panel.transform.localScale = new Vector3(c, c, c);

        StartCoroutine(ConfigDataBase.Instance.ConstructLocalConfigDbFile(Defines.ConfigsFolder, this));
    }

    bool initSuccess = false;
    bool requestManagerSuccess = true;
    int initCounter = 0;

    void AddCounter()
    {
        initCounter++;
    }

    void Initialize()
    {
        if (isInited)
        {
            return;
        }
        isInited = true;

        // 初始化随机工具的seed，int在这里没作用，所有的模板都使用同一个seed
        RandomExt<int>.RandomizeSeed();
        AddCounter();

        LywInt.SetKey(Random.Range(1, int.MaxValue - 1));
        AddCounter();

        ConfigDataBase.Instance.Initialize(new SqliteAccessorFactory(), Defines.ConfigsFolder);
        AddCounter();

        GlobalManager.Instance.Add<LabelTagManager>();
        AddCounter();

        GlobalManager.Instance.Add<DataModelManager>();
        AddCounter();

        GlobalManager.Instance.Add<ConfigUpdateManager>();
        AddCounter();

        GlobalManager.Instance.Add<SceneManager>().AddSceneManagerListener(MachineManager);
        AddCounter();

        GlobalManager.Instance.Add<RequestManager>(
#if UNITY_EDITOR
0f,
#else
10f,
#endif
(System.Action<string>)GameShellManager.Instance.OnRequestManagerBroken, (System.Action<bool>)GameShellManager.Instance.OnRequestManagerBusy);
        AddCounter();
        requestManagerSuccess = true;

        GlobalManager.Instance.Add<ResourceManager>();
        AddCounter();

        GlobalManager.Instance.Add<HotFixManager>();
        AddCounter();

        GlobalManager.Instance.Add<FileManager>();
        AddCounter();

        GlobalManager.Instance.Add<InputManager>("InResource/Constants/EasyTouch"); // TODO : 从配置文件读取路径
        AddCounter();

        GlobalManager.Instance.Add<UiManager>();
        AddCounter();

        GlobalManager.Instance.Add<UiQueueManager>();
        AddCounter();

        GlobalManager.Instance.Add<UiCityPreloadManager>();
        AddCounter();

        GlobalManager.Instance.Add<AlertMessageManager>();
        AddCounter();

        GlobalManager.Instance.Add<IconManager>();
        AddCounter();

        GlobalManager.Instance.Add<FxManager>("AnimationCurve"); // Objects/Other/
        AddCounter();

        GlobalManager.Instance.Add<TimeManager>();
        AddCounter();

        GlobalManager.Instance.Add<ReConnectManager>();
        AddCounter();

        PoolManager poolManager = GlobalManager.Instance.Add<PoolManager>();
        AddCounter();

        SceneManager.Instance.AddSceneManagerListener(poolManager);
        AddCounter();

        UiRegister.UiConfigData[] configDatas = UiRegister.GetAllUiConfigDatas();
        for (int i = 0; i < configDatas.Length; i++)
        {
            UiRelations.Instance.Register(configDatas[i].type, configDatas[i].prefabName, configDatas[i].linkedTypes, configDatas[i].hideOtherModules, configDatas[i].ignoreMutexTypes);
        }

        initSuccess = true;
    }

    void Update()
    {
        if (ConfigDataBase.Instance.ConstructedLocalDbError) // 解压数据库库文件完成，但是出错
        {
            if (!ShowInitFaildPanel("constuct DB failed"))
            {
                LoggerManager.Instance.Info("ConfigDataBase.Instance.ConstructLocalDbError");
            }
            return;
        }

        if (!ConfigDataBase.Instance.ConstructedLocalDb)
        {
            return;
        }

        if (!hasInitedAssetbundles)
        {
            LoggerManager.Instance.Info("init AssetBundleManager");
            GlobalManager.Instance.Add<AssetBundleManager>().StartCoroutine(AssetBundleManager.Instance.InitializeAsync());

            hasInitedAssetbundles = true;
        }

        if (!AssetBundleManager.IsInitialized)
        {
            return;
        }

        if (!isInited)
        {
            Initialize();
        }

        if (!initSuccess)
        {
            if (ShowInitFaildPanel("local failed") == false)
            {
                Debug.LogError("init failed!!!!!!!!!!!!");
            }
            return;
        }

        //走到这里说明游戏初始化已经成功了，现在创建平台
        if (Platform.Instance == null)
        {
            GameMain.Instance.CreatePlatform();
        }
        if (PlatformListener.Instance.IsWaitingForPlatformInitialize())
        {
            return;
        }

        GameStateMachineManager.Instance.EnterState<GameStateQueryGateServer>();
    }

    private bool ShowInitFaildPanel(string text)
    {
        if (!initFaildPanelIsShown)
        {
            initFaildPanelIsShown = true;
            //这里还得用这个方法实现，因为在UIManager初始化之前有一步拷贝配置文件的过程，如果发生意外，这里用UIManager就会有问题
            //这个路径就强制不能变了吧
            Object prefab = Resources.Load<Object>(@"objects/ui/common_ngui/uipnlinitiatefailed");
            GameObject failedPanel = Instantiate(prefab) as GameObject;
            failedPanel.transform.localPosition = Vector3.zero;
            float a = 1, b = 320, c = a / b;
            failedPanel.transform.localScale = new Vector3(c, c, c);

            var obj = UiUtility.FindChild(failedPanel, "Text");
#if UNITY_EDITOR
            if (!requestManagerSuccess)
            {
                obj.GetComponent<UILabel>().text = "CLientHelper模块初始化失败，找服务器去";
            }
            else
            {
                obj.GetComponent<UILabel>().text = "自己看日志， 如果文件被占用就重启unity  ; ";
                obj.GetComponent<UILabel>().text += UnpackFile2PersistentPath.UnPackFileError;
            }
#else

#endif

            return false;
        }

        return true;
    }

}
