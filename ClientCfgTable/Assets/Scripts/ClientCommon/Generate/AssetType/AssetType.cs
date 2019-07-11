namespace ClientCommon
{
    public sealed class AssetType : TypeNameContainer<AssetType>
    {
        /// <summary>
        /// 无效标记
        /// </summary>
        public const int Unknown = 0;
        /// <summary>
        /// 角色
        /// </summary>
        public const int Character = 1;
        /// <summary>
        /// 特效
        /// </summary>
        public const int PFX = 2;
        /// <summary>
        /// 音乐
        /// </summary>
        public const int Music = 3;
        /// <summary>
        /// 音效
        /// </summary>
        public const int Sound = 4;
        /// <summary>
        /// UI
        /// </summary>
        public const int UI = 5;
        /// <summary>
        /// 动画
        /// </summary>
        public const int Animation = 6;
        /// <summary>
        /// 图标
        /// </summary>
        public const int Icon = 7;
        /// <summary>
        /// Other比如动画曲线
        /// </summary>
        public const int Other = 8;
        /// <summary>
        /// 建筑模型
        /// </summary>
        public const int Building = 9;
        /// <summary>
        /// 运行时创建的prefab
        /// </summary>
        public const int RuntimePrefab = 10;
        /// <summary>
        /// 公告文件夹
        /// </summary>
        public const int Advertisement = 12;
        /// <summary>
        /// Shader
        /// </summary>
        public const int Shader = 13;

        public static void Initialize()
        {
            if (initialized)
            {
                return;
            }

            SetTextSectionName("TypeDef_AssetType");

            RegisterType("Unknown", Unknown, "Unknown");
            RegisterType("Character", Character, "Character");
            RegisterType("PFX", PFX, "PFX");
            RegisterType("Music", Music, "Music");
            RegisterType("Sound", Sound, "Sound");
            RegisterType("UI", UI, "UI");
            RegisterType("Animation", Animation, "Animation");
            RegisterType("Icon", Icon, "Icon");
            RegisterType("Other", Other, "Other");
            RegisterType("Building", Building, "Building");
            RegisterType("RuntimePrefab", RuntimePrefab, "RuntimePrefab");
            RegisterType("Advertisement", Advertisement, "Advertisement");
            RegisterType("Shader", Shader, "Shader");

            initialized = true;
        }
    }

}