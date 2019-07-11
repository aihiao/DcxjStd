namespace LywGames
{
    public interface ILywLogger
    {
        string Debug(object msg);
        string Debug(string format, params object[] args);
        string Info(object msg);
        string Info(string format, params object[] args);
        string Warn(object msg);
        string Warn(string format, params object[] args);
        string Error(object msg);
        string Error(string format, params object[] args);

        string AddColor(string msg, string color = "");
        void Write(string msg);
    }
}
