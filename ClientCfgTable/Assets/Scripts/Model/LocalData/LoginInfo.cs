using System.Collections.Generic;
using UnityEngine;
using LywGames.Messages;
using LywGames.Messages.Proto.Auth;

public class LoginInfo
{
    private LoginRes.AreaPro selectedArea;
    public LoginRes.AreaPro SelectedArea
    {
        get { return selectedArea; }
        set { selectedArea = value; }
    }

    private List<LoginRes.AreaPro> areas;
    public List<LoginRes.AreaPro> Areas
    {
        get { return areas; }
        set { areas = value; }
    }

    private LoginRes.ChannelMessage channel;
    public LoginRes.ChannelMessage Channel
    {
        get { return channel; }
        set { channel = value; }
    }

    private string account;
    public string Account
    {
        get { return account; }
        set { account = value; }
    }

    private string passWord;
    public string PassWord
    {
        get { return passWord; }
        set { passWord = value; }
    }

    private long accountId;
    public long AccountId
    {
        get { return accountId; }
        set { accountId = value; }
    }

    private bool isAutoLogin;
    public bool IsAutoLogin
    {
        get { return isAutoLogin; }
        set { isAutoLogin = value; }
    }

    private bool isQuickLogin;
    public bool IsQuickLogin
    {
        get { return isQuickLogin; }
        set { isQuickLogin = value; }
    }

    private bool isFirstQuickLogin;
    public bool IsFirstQuickLogin
    {
        get { return isFirstQuickLogin; }
        set { isFirstQuickLogin = value; }
    }

    private bool isShowActivityInterface;
    public bool IsShowActivityInterface
    {
        get { return isShowActivityInterface; }
        set { isShowActivityInterface = value; }
    }

    private int lastAreaId;
    public int LastAreaId
    {
        get { return lastAreaId; }
        set { lastAreaId = value; }
    }

    private int result;
    public int Result
    {
        get { return result; }
        set { result = value; }
    }

    private string token;
    public string Token
    {
        get { return token; }
        set { token = value; }
    }

    public void SetLoginAuthMessage(ACLoginAuthMessage message)
    {
        accountId = message.Protocol.accountID;
        areas = message.Protocol.areas;
        channel = message.Protocol.channel;
        isFirstQuickLogin = message.Protocol.isFirstQuickLogin;
        isShowActivityInterface = message.Protocol.isShowActiveInterface;
        lastAreaId = message.Protocol.lastAreaID;
        result = message.Protocol.result;
        token = message.Protocol.token;
    }

    public void Initialize()
    {
        Account = PlayerPrefs.GetString(Defines.gdAcc);
        PassWord = PlayerPrefs.GetString(Defines.gdPwd);
        isQuickLogin = PlayerPrefs.GetInt(Defines.gdQuickLogin) == 1 ? true : false;
        IsAutoLogin = PlayerPrefs.GetInt(Defines.gdHasAccountLogined) == 1 ? true : false;
    }

    public void Save()
    {
        PlayerPrefs.SetString(Defines.gdAcc, Account);
        PlayerPrefs.SetString(Defines.gdPwd, PassWord);
        PlayerPrefs.SetInt(Defines.gdQuickLogin, isQuickLogin ? 1 : 0);
        PlayerPrefs.SetInt(Defines.gdHasAccountLogined, IsAutoLogin ? 1 : 0);
    }

}