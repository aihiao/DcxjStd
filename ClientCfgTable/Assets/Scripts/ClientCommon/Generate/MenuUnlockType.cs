namespace ClientCommon
{
    public sealed class MenuUnlockType : TypeNameContainer<MenuUnlockType>
    {
        /// <summary>
        /// VIP等级限制
        /// </summary>
        public const int VIPLevel = 0;
        /// <summary>
        /// 等级限制
        /// </summary>
        public const int Level = 1;
        /// <summary>
        /// 关卡限制功能解锁
        /// </summary>
        public const int Dungeon = 2;
        /// <summary>
        /// 关卡解锁关卡
        /// </summary>
        public const int DungeonUnlock = 3;
        /// <summary>
        /// 完成任务解锁
        /// </summary>
        public const int MissionUnlock = 4;

        public static void Initialize()
        {
            if (initialized)
                return;

            SetTextSectionName("TypeDef_MenuUnlockType");

            RegisterType("VIPLevel", VIPLevel, "VIP等级限制");
            RegisterType("Level", Level, "等级限制");
            RegisterType("Dungeon", Dungeon, "关卡限制功能解锁");
            RegisterType("DungeonUnlock", DungeonUnlock, "关卡解锁关卡");
            RegisterType("MissionUnlock", MissionUnlock, "完成任务解锁");

            initialized = true;
        }
    }
}
