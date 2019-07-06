namespace LywGames
{
    /// <summary>
    /// 单例模板, 需要实现单例模式的类可以从这里派生
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AutoCreateSingleton<T> where T : class, new()
    {
        private static T instance = null;
        public static T Instance
        {
            get { return instance == null ? instance = new T() : instance; }
        }
    }
}