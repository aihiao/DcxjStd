namespace ClientCommon
{
    public sealed class AlertMessageType : TypeNameContainer<AlertMessageType>
    {
        /// <summary>
        /// 无效标记
        /// </summary>
        public const int Unknown = 0;
        /// <summary>
        /// 飘字
        /// </summary>
        public const int FlowTip = 1;
        /// <summary>
        /// 弹窗
        /// </summary>
        public const int Popup = 2;

        public static void Initialize()
        {
            if (initialized)
                return;

            SetTextSectionName("TypeDef_AlertMessageType");

            RegisterType("Unknown", Unknown, "Unknown");
            RegisterType("FlowTip", FlowTip, "FlowTip");
            RegisterType("Popup", Popup, "Popup");

            initialized = true;
        }
    }
}
