namespace ClientCommon
{
    public sealed class AnimationEvtHandlerFunc : TypeNameContainer<AnimationEvtHandlerFunc>
    {
        /// <summary>
        /// 0
        /// </summary>
        public const int None = 0;
        /// <summary>
        /// 0
        /// </summary>
        public const int PlaySound = 1;
        /// <summary>
        /// 0
        /// </summary>
        public const int PlayMusic = 2;
        /// <summary>
        /// 0
        /// </summary>
        public const int StopMusic = 3;
        /// <summary>
        /// 0
        /// </summary>
        public const int StepMusicToNormalState = 4;
        /// <summary>
        /// 0
        /// </summary>
        public const int SetAnimationSpeed = 5;
        /// <summary>
        /// 0
        /// </summary>
        public const int ShakeMainCamera = 6;
        /// <summary>
        /// 0
        /// </summary>
        public const int ShakeSelf = 7;
        /// <summary>
        /// 0
        /// </summary>
        public const int PlayFx = 8;
        /// <summary>
        /// 0
        /// </summary>
        public const int AutoDestroy = 9;
        /// <summary>
        /// 0
        /// </summary>
        public const int UserDefinedFunction = 10;
        /// <summary>
        /// 0
        /// </summary>
        public const int MarkKeyFrame = 11;
        /// <summary>
        /// 0
        /// </summary>
        public const int PlayAbilityPfx = 12;
        /// <summary>
        /// 0
        /// </summary>
        public const int MarkLogicFrame = 13;

        public static void Initialize()
        {
            if (initialized)
                return;

            SetTextSectionName("TypeDef_AnimationEvtHandlerFunc");

            RegisterType("None", None, "None");
            RegisterType("PlaySound", PlaySound, "播放声音");
            RegisterType("PlayMusic", PlayMusic, "播放音乐");
            RegisterType("StopMusic", StopMusic, "停止音乐");
            RegisterType("StepMusicToNormalState", StepMusicToNormalState, "StepMusicToNormalState");
            RegisterType("SetAnimationSpeed", SetAnimationSpeed, "设置动画速度");
            RegisterType("ShakeMainCamera", ShakeMainCamera, "震动相机");
            RegisterType("ShakeSelf", ShakeSelf, "震动自身");
            RegisterType("PlayFx", PlayFx, "播放特效");
            RegisterType("AutoDestroy", AutoDestroy, "自动销毁");
            RegisterType("UserDefinedFunction", UserDefinedFunction, "自定义");
            RegisterType("MarkKeyFrame", MarkKeyFrame, "标记关键帧");
            RegisterType("PlayAbilityPfx", PlayAbilityPfx, "播放特效（新）");
            RegisterType("MarkLogicFrame", MarkLogicFrame, "标记逻辑关键帧");

            initialized = true;
        }
    }
}
