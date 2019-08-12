namespace LywGames.Messages
{
    public class NetworkParameters
    {
        public static int MaxRecvBufferSize = 102400; // 1024 * 100 = 100KB
        public static int MaxSendBufferSize = 2048;
        public static int MaxCompressMessageSize = 196611;
        public static int MaxUncompressMessageSize = 1048576; // 1024 * 1024 = 1048576
    }
}
