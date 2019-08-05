using LywGames.Messages;
using LywGames.Corgi.Protocol;

/// <summary>
/// 激活码激活请求
/// 这个是连接的AS，不是GS
/// </summary>
public class CAActiveCodeReq : BaseRequest
{
    public CAActiveCodeReq(string asHost, int asPort, string activeCode)
    {
        host = asHost;
        port = asPort;
        callback = CallBackId;
        code = activeCode;
        id = DataModelManager.Instance.LoginInfo.AccountId;
    }

    private string host, code;
    private int port, callback;
    private long id;

    public override bool Execute(ServerBusiness bsn)
    {
        return bsn.ActiveCodeReq(host, port, callback, id, code);
    }
}

/// <summary>
/// 激活码激活的消息回复
/// </summary>
public class ACActiveCodeRes : AbsResponse<ACActiveCodeMessage>
{
    public override void Execute(BaseRequest request)
    {
        switch (result)
        {
            case Protocols.AuthActiveCodeSuccess:
                {
                    UiManager.Instance.Hide<UiPnlActivationCode>();     //关闭激活码界面

                    if (UiManager.Instance.GetIsShowing<UiPnlLogin>())  //关闭登陆界面
                        UiManager.Instance.GetUi<UiPnlLogin>().OnLoginSuccess();

                    if (UiManager.Instance.GetIsShowing<UiPnlAreaChoose>()) //关闭选区界面
                        UiManager.Instance.Hide<UiPnlAreaChoose>();

                    GameStateMachineManager.Instance.EnterState<GameStateSelectArea>().StartUi();  //进入选区界面
                }
                break;

        }
    }
}
