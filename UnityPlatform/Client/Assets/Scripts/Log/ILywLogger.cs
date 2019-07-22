namespace LywGames
{
    public interface ILywLogger
    {
        string Debug(string format, params object[] args);
        string Info(string format, params object[] args);
        string Warn(string format, params object[] args);
        string Error(string format, params object[] args);

        string AddColor(string msg, string color = "");
        void Write(string msg);
    }
}
