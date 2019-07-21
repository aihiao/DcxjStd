using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LywGames
{
    /// <summary>
    /// 发布作者名称类
    /// </summary>
    public class ProductPublisher
    {
        public const int Local = 0;
        public const int LywGames = 1;
    }

    /// <summary>
	/// 用于从外部(Android,iOS)获取相关配置
	/// </summary>
    public class LywConfig
    {
        [DllImport("__Internal")]
        private static extern int UnityCall_LywConfig_GetPublisher();
        [DllImport("__Internal")]
        private static extern IntPtr UnityCall_LywConfig_GetServerIP();
        [DllImport("__Internal")]
        private static extern int UnityCall_LywConfig_GetServerPort();

        private static AndroidJavaClass javaCls = null;
        private static AndroidJavaClass GetJavaCls()
        {
            return javaCls == null ? javaCls = new AndroidJavaClass("") : javaCls;
        }

        public static int GetPublisher()
        {
#if UNITY_IPHONE
            UnityCall_LywConfig_GetPublisher();
#elif UNITY_ANDROID
            GetJavaCls().CallStatic<int>("getPublisher");
#else
            return ProductPublisher.Local;
#endif
        }

        public static string GetServerIP()
        {
#if UNITY_IPHONE
            Marshal.PtrToStringAnsi(UnityCall_LywConfig_GetServerIP());
#elif UNITY_ANDROID
            GetJavaCls().CallStatic<string>("getServerIP");
#else
            return "127.0.0.1";
#endif
        }

        public static int GetServerPort()
        {
#if UNITY_IPHONE
            UnityCall_LywConfig_GetServerPort();
#elif UNITY_ANDROID
            GetJavaCls().CallStatic<int>("getServerPort");
#else
            return 3771;
#endif
        }

        public static void Exit(string title, string message, string okText, string cancelText)
        {
#if UNITY_ANDROID
			GetJavaClass().CallStatic("show", title, message, okText, cancelText);
#endif
        }

    }
}