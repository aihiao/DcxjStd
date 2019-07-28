namespace LywGames.Corgi.Protocol
{
    public class Protocols 
    {
        public static bool isSuccess(int id)
        {
            return id <= 0 || ((long)id & (long)(-1048576)) >> 20 < 16L;
        }
    }
}