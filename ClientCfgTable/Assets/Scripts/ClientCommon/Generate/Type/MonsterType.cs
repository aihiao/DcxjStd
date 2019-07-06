namespace ClientCommon
{
    public sealed class MonsterType : TypeNameContainer<MonsterType>
    {
        /// <summary>
        /// 无
        /// </summary>
        public const int Unknown = 1;
        /// <summary>
        /// 最大血量值
        /// </summary>
        public const int MAXHP = 2;
        /// <summary>
        /// 血量值
        /// </summary>
        public const int HP = 3;
        /// <summary>
        /// 攻击值
        /// </summary>
        public const int AP = 4;
        /// <summary>
        /// 内功防御值
        /// </summary>
        public const int IDP = 5;
        /// <summary>
        /// 外功防御值
        /// </summary>
        public const int ODP = 6;
        /// <summary>
        /// 内功破防值
        /// </summary>
        public const int IABP = 7;
        /// <summary>
        /// 外功破防值
        /// </summary>
        public const int OABP = 8;
        /// <summary>
        /// 命中值
        /// </summary>
        public const int HITP = 9;
        /// <summary>
        /// 闪避值
        /// </summary>
        public const int DGP = 10;
        /// <summary>
        /// 暴击值
        /// </summary>
        public const int CP = 11;
        /// <summary>
        /// 抗暴值
        /// </summary>
        public const int CRP = 12;
        /// <summary>
        /// 暴击伤害值
        /// </summary>
        public const int CDP = 13;
        /// <summary>
        /// 免伤值
        /// </summary>
        public const int AVOP = 14;
        /// <summary>
        /// 攻击速度值
        /// </summary>
        public const int ASP = 15;
        /// <summary>
        /// 移动速度值
        /// </summary>
        public const int SP = 16;

        public static void Initialize()
        {
            if (initialized)
                return;

            SetTextSectionName("TypeDef_MonsterType");

            RegisterType("Unknown", Unknown, "Unknown");
            RegisterType("MAXHP", MAXHP, "MAXHP");
            RegisterType("HP", HP, "气血");
            RegisterType("AP", AP, "伤害");
            RegisterType("IDP", IDP, "内功防御");
            RegisterType("ODP", ODP, "外功防御");
            RegisterType("IABP", IABP, "内功破防");
            RegisterType("OABP", OABP, "外功破防");
            RegisterType("HITP", HITP, "命中");
            RegisterType("DGP", DGP, "躲闪");
            RegisterType("CP", CP, "会心");
            RegisterType("CRP", CRP, "韧性");
            RegisterType("CDP", CDP, "会心伤害");
            RegisterType("AVOP", AVOP, "免伤");
            RegisterType("ASP", ASP, "攻击速度");
            RegisterType("SP", SP, "移动速度");

            initialized = true;
        }
    }

}
