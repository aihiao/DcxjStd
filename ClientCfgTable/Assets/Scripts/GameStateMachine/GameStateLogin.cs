using UnityEngine;

public class GameStateLogin : GameStateBase
{
    public override bool IsGamingState
    {
        get
        {
            return false;
        }
    }

    public override GameStateType StateType
    {
        get
        {
            return GameStateType.Login;
        }
    }

    public void OnLoginSuccess(PRLoginAS prlogin)
    {

    }

    public void OnLoginFailed(int errCode, string errMsg)
    {

    }

    public void CreateAccount(string account, string password, bool isPasswordEncrypted, int passwordLength)
    {

    }

    public void OnCreateAccountSuccess(string account, string password, bool isPasswordEncrypted, int passwordLength)
    {

    }

    public void OnCreateAccountFalied(int errCode, string errMsg)
    {

    }

}