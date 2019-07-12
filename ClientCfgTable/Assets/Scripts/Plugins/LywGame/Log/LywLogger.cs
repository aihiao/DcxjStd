using System;
using System.IO;

namespace LywGames
{
    public class LywLogger : ILywLogger
    {
        private string logFilePath = "logger/";
        private string logFileExtension = ".txt";

        public LywLogger()
        {
            logFilePath += DateTime.Now.Ticks;
            logFilePath += logFileExtension;
            logFilePath = FileManager.GetPersistentPath(logFilePath);
        }

        public string Debug(string format, params object[] args)
        {
            return Format(format, LogLevel.Debug.ToString(), args);
        }

        public string Info(string format, params object[] args)
        {
            return Format(format, LogLevel.Info.ToString(), args);
        }

        public string Warn(string format, params object[] args)
        {
            return Format(format, LogLevel.Warning.ToString(), args);
        }

        public string Error(string format, params object[] args)
        {
            return Format(format, LogLevel.Error.ToString(), args);
        }

        private string Format(string format, string tag, params object[] args)
        {
            return string.Format("[{0}] [{1}]:{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), tag, string.Format(format, args));
        }

        public string AddColor(string msg, string color = "")
        {
            return (!string.IsNullOrEmpty(color)) ? ("<color=" + color + ">" + msg + "</color>") : msg;
        }

        public void Write(string msg)
        {
            if (!PathUtility.CreateFile(logFilePath))
            {
                return;
            }

            using (StreamWriter streamWriter = File.AppendText(logFilePath))
            {
                streamWriter.WriteLine(msg + "\t\n");
                streamWriter.Close();
            }
        }

    }
}
