namespace ClientCommon
{
    public sealed class AttributeType : TypeNameContainer<AttributeType>
    {
        /// <summary>
        /// 无
        /// </summary>
        public const int Unknown = 1;
        /// <summary>
        /// Reserved
        /// </summary>
        public const int Reserved = 2;
        /// <summary>
        /// 气血
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
        /// <summary>
        /// 分隔符
        /// </summary>
        public const int NUMBER_RATE_SPACE = 17;
        /// <summary>
        /// 内功防御率
        /// </summary>
        public const int IDR = 18;
        /// <summary>
        /// 外功防御率
        /// </summary>
        public const int ODR = 19;
        /// <summary>
        /// 内功破防率
        /// </summary>
        public const int IABR = 20;
        /// <summary>
        /// 外功破防率
        /// </summary>
        public const int OABR = 21;
        /// <summary>
        /// 命中率
        /// </summary>
        public const int HITR = 22;
        /// <summary>
        /// 闪避率
        /// </summary>
        public const int DGR = 23;
        /// <summary>
        /// 暴击率
        /// </summary>
        public const int CR = 24;
        /// <summary>
        /// 抗暴率
        /// </summary>
        public const int CRR = 25;
        /// <summary>
        /// 暴击伤害率
        /// </summary>
        public const int CDR = 26;
        /// <summary>
        /// 免伤率
        /// </summary>
        public const int AVOR = 27;
        /// <summary>
        /// 攻击速度率
        /// </summary>
        public const int ASR = 28;
        /// <summary>
        /// 移动速度率
        /// </summary>
        public const int SR = 29;
        /// <summary>
        /// attribute_type数量
        /// </summary>
        public const int NUMBER = 30;

        public static void Initialize()
        {
            if (initialized)
            {
                return;
            }

            SetTextSectionName("TypeDef_AttributeType");

            RegisterType("Unknown", Unknown, "Unknown");
            RegisterType("Reserved", Reserved, "Reserved");
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
            RegisterType("NUMBER_RATE_SPACE", NUMBER_RATE_SPACE, "------------------------");
            RegisterType("IDR", IDR, "内功防御率");
            RegisterType("ODR", ODR, "外功防御率");
            RegisterType("IABR", IABR, "内功破防率");
            RegisterType("OABR", OABR, "外功破防率");
            RegisterType("HITR", HITR, "命中率");
            RegisterType("DGR", DGR, "闪避率");
            RegisterType("CR", CR, "暴击率");
            RegisterType("CRR", CRR, "抗暴率");
            RegisterType("CDR", CDR, "暴击伤害率");
            RegisterType("AVOR", AVOR, "免伤率");
            RegisterType("ASR", ASR, "攻击速度率");
            RegisterType("SR", SR, "移动速度率");
            RegisterType("NUMBER", NUMBER, "attribute_type数量");

            initialized = true;
        }
    }

}
