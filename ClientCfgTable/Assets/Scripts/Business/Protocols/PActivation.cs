using LywGames.Messages;
using LywGames.Corgi.Protocol;

/// <summary>
/// 激活码激活请求, 连接AS
/// </summary>
public class CAActiveCodeReq : BaseRequest
{
    private string asHostName, activeCode;
    private int asHostPort, callBackId;
    private long accountId;

    public CAActiveCodeReq(string asHostName, int asHostPort, string activeCode)
    {
        this.asHostName = asHostName;
        this.asHostPort = asHostPort;
        this.activeCode = activeCode;
        callBackId = CallBackId;
        accountId = DataModelManager.Instance.LoginInfo.AccountId;
    }

    public override bool Execute(ServerBusiness bsn)
    {
        return bsn.ActiveCodeAS(asHostName, asHostPort, callBackId, accountId, activeCode);
    }
}

/// <summary>
/// 激活码激活的消息回复
/// </summary>
public class ACActiveCodeRes : AbsResponse<ACActiveCodeMessage>
{
    public override void Execute(BaseRequest request)
    {
        switch (ResultCode)
        {
            case Protocols.AuthActiveCodeSuccess:
                {
                    // 关闭激活码界面
                    UiManager.Instance.Hide<UiPnlActivationCode>();

                    // 关闭登陆界面
                    if (UiManager.Instance.GetIsShowing<UiPnlLogin>())  
                    {
                        UiManager.Instance.GetUi<UiPnlLogin>().OnLoginSuccess();
                    }

                    // 关闭选区界面
                    if (UiManager.Instance.GetIsShowing<UiPnlAreaChoose>()) 
                    {
                        UiManager.Instance.Hide<UiPnlAreaChoose>();
                    }

                    // 进入选区界面
                    GameStateMachineManager.Instance.EnterState<GameStateSelectArea>().StartUi();  
                }
            break;
        }
    }
}
