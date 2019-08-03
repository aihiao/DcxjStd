namespace ClientCommon
{
    public sealed class CreateObjectPlacementType : TypeNameContainer<CreateObjectPlacementType>
    {
        /// <summary>
        /// 0
        /// </summary>
        public const int Unknown = 0;
        /// <summary>
        /// 0
        /// </summary>
        public const int AtTarget = 1;
        /// <summary>
        /// 0
        /// </summary>
        public const int AtSource = 2;

        public static void Initialize()
        {
            if (initialized)
                return;

            SetTextSectionName("TypeDef_CreateObjectPlacementType");

            RegisterType("Unknown", Unknown, "无效");
            RegisterType("AtTarget", AtTarget, "目标");
            RegisterType("AtSource", AtSource, "源");

            initialized = true;
        }
    }
}
