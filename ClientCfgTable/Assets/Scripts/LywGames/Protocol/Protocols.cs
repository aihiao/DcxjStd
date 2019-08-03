namespace LywGames.Corgi.Protocol
{
    public class Protocols 
    {
        public const int GameLoginSuccess = 1179651;
        public const int GameLoginSuccessRoleNotExist = 2228227;
        public const int GameLoginFail = 16908291;
        public const int GameLoginFailNeedActiveCode = 19005443;
        public const int GameLoginFromOthers = 17956867;
        public const int GameLoginTableVersionCheckSuccessUpdate = 3276803;

        public const int TableVersionCheckSuccess = 1179658;
        public const int TableVersionCheckSuccessUpdate = 2228234;
        public const int TableVersionCheckFail = 16908298;

        public static bool isSuccess(int id)
        {
            return id <= 0 || ((long)id & (long)(-1048576)) >> 20 < 16L;
        }
    }
}