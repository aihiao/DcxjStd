using UnityEngine;

/// <summary>
/// 登录界面
/// </summary>
public class UiPnlLogin : BaseUi
{
    private UIInput inputName;
    private UIInput inputPassWord;
    private UIButton loginBtn;
    private UIButton registerBtn;
    private GameObject backgroundObj;
    private GameObject clickToStart;
    private UIWidget fullScreenClick;
    private UILabel versionText;

    public void OnLoginSuccess()
    {
        PlayerSaveData.Destroy();   //登录成功需要使用新号的本地数据  放在这清保险
        Hide();
    }

}
