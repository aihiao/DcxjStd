namespace ClientCommon
{
    public sealed class CreateObjectFacingType : TypeNameContainer<CreateObjectFacingType>
    {
        /// <summary>
        /// 0
        /// </summary>
        public const int Unknown = 0;
        /// <summary>
        /// 0
        /// </summary>
        public const int ToTarget = 1;
        /// <summary>
        /// 0
        /// </summary>
        public const int InheritTarget = 2;
        /// <summary>
        /// 0
        /// </summary>
        public const int ToSource = 3;
        /// <summary>
        /// 0
        /// </summary>
        public const int InheritSource = 4;
        /// <summary>
        /// 位置朝向与绑定的骨骼一致
        /// </summary>
        public const int InheritBone = 5;
        /// <summary>
        /// 仅位置与绑定的骨骼一致
        /// </summary>
        public const int InheritBonePositionOnly = 6;

        public static void Initialize()
        {
            if (initialized)
                return;

            SetTextSectionName("TypeDef_CreateObjectFacingType");

            RegisterType("Unknown", Unknown, "无效");
            RegisterType("ToTarget", ToTarget, "朝向目标");
            RegisterType("InheritTarget", InheritTarget, "与目标相同");
            RegisterType("ToSource", ToSource, "朝向源");
            RegisterType("InheritSource", InheritSource, "与源相同");
            RegisterType("InheritBone", InheritBone, "位置朝向与绑定的骨骼一致");
            RegisterType("InheritBonePositionOnly", InheritBonePositionOnly, "仅位置与绑定的骨骼一致");

            initialized = true;
        }
    }
}
