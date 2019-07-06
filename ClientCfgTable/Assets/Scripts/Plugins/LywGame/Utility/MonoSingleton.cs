using UnityEngine;

namespace LywGames
{
    /// <summary>
    /// 基于MonoBehaviour的单例基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance = null;
        public static T Instance { get { return instance; } }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
        }

        protected virtual void OnApplicationQuit()
        {
            instance = null;
        }

    }
}
