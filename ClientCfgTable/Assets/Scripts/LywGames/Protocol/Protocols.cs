namespace LywGames.Corgi.Protocol
{
    public class Protocols 
    {
        public const int P_CA_AuthQuickLogin = 65537;
        public const int P_CA_AuthLogin = 65538;
        public const int P_AC_AuthLogin = 65539;

        public const int P_CA_CreateAccount = 65540;
        public const int P_AC_CreateAccount = 65541;

        public const int P_CA_BindAccount = 65542;
        public const int P_AC_BindAccount = 65543;
        public const int AuthBindAccountSuccess = 1114119;

        public const int P_CA_ChangePassword = 65544;
        public const int P_AC_ChangePassword = 65545;
        public const int AuthChangePasswordSuccess = 1114121;
        public const int AuthChangePasswordFailed = 16842761;
        
        public const int P_CA_ActiveCode = 65546;
        public const int P_AC_ActiveCode = 65547;
        public const int AuthActiveCodeSuccess = 1114123;
        public const int AuthActiveCodeFailed = 16842763;

        public const int GameLoginSuccess = 1179651;
        public const int GameLoginSuccessRoleNotExist = 2228227;
        public const int GameLoginFail = 16908291;
        public const int GameLoginFailNeedActiveCode = 19005443;
        public const int GameLoginFromOthers = 17956867;
        public const int GameLoginTableVersionCheckSuccessUpdate = 3276803;

        public const int TableVersionCheckSuccess = 1179658;
        public const int TableVersionCheckSuccessUpdate = 2228234;
        public const int TableVersionCheckFail = 16908298;


        public const int P_CG_GameLogin = 131074; // Protocol Client to Game Server, Login Game Server.
        public const int P_GC_GameLogin = 131075; // Protocol Game Server to Client, Login Game Server.
        public const int P_CG_GameLogout = 131076;
        public const int P_GC_GameLogout = 131077;

        public const int P_GC_StaminaBuyChange = 131307;
        public const int P_CG_StaminaBuyInfo = 131308;
        public const int P_GC_StaminaBuyInfo = 131309;

        public static bool isSuccess(int id)
        {
            return id <= 0 || ((long)id & (long)(-1048576)) >> 20 < 16L;
        }
    }
}