namespace ClientCommon
{
    public sealed class MenuGainType : TypeNameContainer<MenuGainType>
    {
        /// <summary>
        /// 无跳转
        /// </summary>
        public const int None = 0;
        /// <summary>
        /// 功能跳转
        /// </summary>
        public const int Function = 1;
        /// <summary>
        /// 剧情副本关卡
        /// </summary>
        public const int DungeonPoint = 2;
        /// <summary>
        /// 剧情副本章节
        /// </summary>
        public const int DungeonChapater = 3;
        /// <summary>
        /// 子功能获取跳转
        /// </summary>
        public const int SubFunction = 4;

        public static void Initialize()
        {
            if (initialized)
                return;

            SetTextSectionName("TypeDef_MenuGainType");

            RegisterType("None", None, "无跳转");
            RegisterType("Function", Function, "功能跳转");
            RegisterType("DungeonPoint", DungeonPoint, "剧情副本关卡");
            RegisterType("DungeonChapater", DungeonChapater, "剧情副本章节");
            RegisterType("SubFunction", SubFunction, "SubFunction");

            initialized = true;
        }
    }
}
