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
    public Dictionary<LogLevel, bool> LogLevelEnabledDic { get { return logLevelEnabledDic; } }

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

    public void Debug(string format, params object[] args)
    {
        DebugColor(format, string.Empty, null, args);
    }

    public void DebugColor(string format, string color, params object[] args)
    {
        DebugColor(format, color, null, args);
    }

    public void DebugColor(string format, string color, Object context, params object[] args)
    {
        Log(LogLevel.Debug, logger.Debug(format, args), color, context);
    }

    public void Info(string format, params object[] args)
    {
        InfoColor(format, string.Empty, null, args);
    }

    public void InfoColor(string format, string color, params object[] args)
    {
        InfoColor(format, color, null, args);
    }

    public void InfoColor(string format, string color, Object context = null, params object[] args)
    {
        Log(LogLevel.Info, logger.Info(format, args), color, context);
    }

    public void Warn(string format, params object[] args)
    {
        WarnColor(format, string.Empty, null, args);
    }

    public void WarnColor(string format, string color, params object[] args)
    {
        WarnColor(format, color, null, args);
    }

    public void WarnColor(string format, string color, Object context, params object[] args)
    {
        Log(LogLevel.Warning, logger.Warn(format, args), color, context);
    }

    public void Error(string format, params object[] args)
    {
        ErrorColor(format, string.Empty, null, args);
    }

    public void ErrorColor(string format, string color, params object[] args)
    {
        ErrorColor(format, color, null, args);
    }

    public void ErrorColor(string format, string color, Object context, params object[] args)
    {
        Log(LogLevel.Error, logger.Error(format, args), color, context);
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
