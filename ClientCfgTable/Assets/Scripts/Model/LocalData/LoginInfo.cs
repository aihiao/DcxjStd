using System;
using System.Collections.Generic;
using System.Linq;
using LywGames.Message.Proto.Auth;

public class LoginInfo
{
    private LoginRes.AreaPro selectedArea;
    public LoginRes.AreaPro SelectedArea
    {
        get { return selectedArea; }
        set { selectedArea = value; }
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

    private long accountID;
    public long AccountID
    {
        get { return accountID; }
        set { accountID = value; }
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

    public void Initialize()
    {

    }

    public void Save()
    {

    }

}