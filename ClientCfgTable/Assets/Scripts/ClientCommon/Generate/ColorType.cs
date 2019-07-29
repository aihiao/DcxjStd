namespace ClientCommon
{
    public sealed class ColorType : TypeNameContainer<ColorType>
    {
        /// <summary>
        /// 未知
        /// </summary>
        public const int Unknow = 0;
        /// <summary>
        /// 浅蓝
        /// </summary>
        public const int LightBlue = 1;
        /// <summary>
        /// 红色
        /// </summary>
        public const int Red = 2;
        /// <summary>
        /// 绿色
        /// </summary>
        public const int Green = 3;
        /// <summary>
        /// 棕色
        /// </summary>
        public const int Brown = 4;
        /// <summary>
        /// 黄色
        /// </summary>
        public const int Yellow = 5;
        /// <summary>
        /// 土黄色
        /// </summary>
        public const int EarthyYellow = 6;
        /// <summary>
        /// 橘黄
        /// </summary>
        public const int Orange = 7;

        public static void Initialize()
        {
            if (initialized)
                return;

            SetTextSectionName("TypeDef_ColorType");

            RegisterType("Unknow", Unknow, "未知");
            RegisterType("LightBlue", LightBlue, "[00BAFF]{0}[-]");
            RegisterType("Red", Red, "[FF0000]{0}[-]");
            RegisterType("Green", Green, "[00FF00]{0}[-]");
            RegisterType("Brown", Brown, "[46260A]{0}[-]");
            RegisterType("Yellow", Yellow, "[FFFC01]{0}[-]");
            RegisterType("EarthyYellow", EarthyYellow, "[F39700]{0}[-]");
            RegisterType("Orange", Orange, "[FF9E00]{0}[-]");

            initialized = true;
        }
    }
}
