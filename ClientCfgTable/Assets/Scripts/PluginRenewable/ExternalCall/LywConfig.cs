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
        [DllImport("__Internal")]
        private static extern int UnityCall_LywConfig_GetDeviceConnectedInternetStatus();

        private static AndroidJavaClass javaCls = null;
        private static AndroidJavaClass GetJavaCls()
        {
            return javaCls == null ? javaCls = new AndroidJavaClass("com.lywgames.LywConfig") : javaCls;
        }

        public static int GetPublisher()
        {
#if UNITY_IPHONE
            return UnityCall_LywConfig_GetPublisher();
#elif UNITY_ANDROID
            return GetJavaCls().CallStatic<int>("getPublisher");
#else
            return ProductPublisher.Local;
#endif
        }

        public static string GetServerIP()
        {
#if UNITY_IPHONE
            return Marshal.PtrToStringAnsi(UnityCall_LywConfig_GetServerIP());
#elif UNITY_ANDROID
            return GetJavaCls().CallStatic<string>("getServerIP");
#else
            return "127.0.0.1";
#endif
        }

        public static int GetServerPort()
        {
#if UNITY_IPHONE
            return UnityCall_LywConfig_GetServerPort();
#elif UNITY_ANDROID
            return GetJavaCls().CallStatic<int>("getServerPort");
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

        /// <summary>
		/// 获得设备网络状态
		/// 0 未联网
		/// 1 wifi联网
		/// 2 移动网络
		/// </summary>
		public static int GetDeviceConnectedInternetStatus()
        {
#if UNITY_IPHONE
            return UnityCall_LywConfig_GetDeviceConnectedInternetStatus();
#elif UNITY_ANDROID
            return GetJavaCls().CallStatic<int>("getDeviceConnectedInternetStatus");
#else
            return 0;
#endif
        }

    }
}