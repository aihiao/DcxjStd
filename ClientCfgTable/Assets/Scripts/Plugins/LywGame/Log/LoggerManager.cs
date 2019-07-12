using System.Collections.Generic;
using UnityEngine;
using LywGames;

public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error
}

public class LoggerManager
{ 
    private ILywLogger logger;
    private static Dictionary<LogLevel, bool> logLevelEnabledDic = new Dictionary<LogLevel, bool>();

    private static LoggerManager instance = null;
    public static LoggerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LoggerManager();
            }
            return instance;
        }
    }
    private LoggerManager()
    {
        logger = new LywLogger();

#if UNITY_EDITOR
        logLevelEnabledDic[LogLevel.Debug] = true;
        logLevelEnabledDic[LogLevel.Info] = true;
        logLevelEnabledDic[LogLevel.Warning] = true;
        logLevelEnabledDic[LogLevel.Error] = true;
#else
        logLevelEnabledDic[LogLevel.Warning] = true;
        logLevelEnabledDic[LogLevel.Error] = true;
#endif
    }

    private bool write2File = false;
    public bool Write2File
    {
        get
        {
            return write2File;
        }
        set
        {
            write2File = value;
        }
    }

    public void Debug(object msg, string color = "", Object context = null)
    {
        Log(LogLevel.Debug, logger.Debug(msg), color);
    }

    public void Debug(string format, params object[] args)
    {
        Debug(format, string.Empty, null, args);
    }

    public void Debug(string format, string color = "", params object[] args)
    {
        Debug(format, color, null, args);
    }

    public void Debug(string format, string color = "", Object context = null, params object[] args)
    {
        Log(LogLevel.Debug, logger.Debug(format, args), color);
    }

    public void Info(object msg, string color = "", Object context = null)
    {
        Log(LogLevel.Info, logger.Info(msg), color);
    }

    public void Info(string format, params object[] args)
    {
        Info(format, string.Empty, null, args);
    }

    public void Info(string format, string color = "", params object[] args)
    {
        Info(format, color, null, args);
    }

    public void Info(string format, string color = "", Object context = null, params object[] args)
    {
        Log(LogLevel.Info, logger.Info(format, args), color);
    }

    public void Warn(object msg, string color = "", Object context = null)
    {
        Log(LogLevel.Warning, logger.Warn(msg), color);
    }

    public void Warn(string format, params object[] args)
    {
        Warn(format, string.Empty, null, args);
    }

    public void Warn(string format, string color = "", params object[] args)
    {
        Warn(format, color, null, args);
    }

    public void Warn(string format, string color = "", Object context = null, params object[] args)
    {
        Log(LogLevel.Warning, logger.Warn(format, args), color);
    }

    public void Error(object msg, string color = "", Object context = null)
    {
        Log(LogLevel.Error, logger.Error(msg), color);
    }

    public void Error(string format, params object[] args)
    {
        Error(format, string.Empty, null, args);
    }

    public void Error(string format, string color = "", params object[] args)
    {
        Error(format, color, null, args);
    }

    public void Error(string format, string color = "", Object context = null, params object[] args)
    {
        Log(LogLevel.Error, logger.Error(format, args), color);
    }

    private bool IsLogLevelEnabled(LogLevel logLevel)
    {
        return logLevelEnabledDic[logLevel];
    }

    private void Log(LogLevel logLevel, string msg, string color = "",  Object context = null)
    {
        if (IsLogLevelEnabled(logLevel))
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    UnityEngine.Debug.Log(logger.AddColor(msg, color), context);
                    break;
                case LogLevel.Info:
                    UnityEngine.Debug.Log(logger.AddColor(msg, color), context);
                    break;
                case LogLevel.Warning:
                    UnityEngine.Debug.LogWarning(logger.AddColor(msg, color), context);
                    break;
                case LogLevel.Error:
                    UnityEngine.Debug.LogError(logger.AddColor(msg, color), context);
                    break;
            }

            if (write2File)
            {
                logger.Write(msg);
            }
        }
    }

}
