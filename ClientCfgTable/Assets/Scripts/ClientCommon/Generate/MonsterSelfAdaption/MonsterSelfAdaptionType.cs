namespace ClientCommon
{
    public sealed class MonsterSelfAdaptionType : TypeNameContainer<MonsterSelfAdaptionType>
    {
        /// <summary>
        /// 为了自动生成代码时monsterSelfadaption这个表能正确生成而加的枚举，并卵
        /// </summary>
        public const int Unknown = 1;
        /// <summary>
        /// 0
        /// </summary>
        public const int MAXHP = 2;
        /// <summary>
        /// 0
        /// </summary>
        public const int HP = 3;
        /// <summary>
        /// 0
        /// </summary>
        public const int AP = 4;
        /// <summary>
        /// 0
        /// </summary>
        public const int IDP = 5;
        /// <summary>
        /// 0
        /// </summary>
        public const int ODP = 6;
        /// <summary>
        /// 0
        /// </summary>
        public const int IABP = 7;
        /// <summary>
        /// 0
        /// </summary>
        public const int OABP = 8;
        /// <summary>
        /// 0
        /// </summary>
        public const int HITP = 9;
        /// <summary>
        /// 0
        /// </summary>
        public const int DGP = 10;
        /// <summary>
        /// 0
        /// </summary>
        public const int CP = 11;
        /// <summary>
        /// 0
        /// </summary>
        public const int CRP = 12;
        /// <summary>
        /// 0
        /// </summary>
        public const int CDP = 13;
        /// <summary>
        /// 0
        /// </summary>
        public const int AVOP = 14;
        /// <summary>
        /// 0
        /// </summary>
        public const int ASP = 15;
        /// <summary>
        /// 0
        /// </summary>
        public const int SP = 16;

        public static void Initialize()
        {
            if (initialized)
                return;

            SetTextSectionName("TypeDef_MonsterSelfAdaptionType");

            RegisterType("Unknown", Unknown, "Unknown");
            RegisterType("MAXHP", MAXHP, "MAXHP");
            RegisterType("HP", HP, "HP");
            RegisterType("AP", AP, "AP");
            RegisterType("IDP", IDP, "IDP");
            RegisterType("ODP", ODP, "ODP");
            RegisterType("IABP", IABP, "IABP");
            RegisterType("OABP", OABP, "OABP");
            RegisterType("HITP", HITP, "HITP");
            RegisterType("DGP", DGP, "DGP");
            RegisterType("CP", CP, "CP");
            RegisterType("CRP", CRP, "CRP");
            RegisterType("CDP", CDP, "CDP");
            RegisterType("AVOP", AVOP, "AVOP");
            RegisterType("ASP", ASP, "ASP");
            RegisterType("SP", SP, "SP");

            initialized = true;
        }
    }

}
