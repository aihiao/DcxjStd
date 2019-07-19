namespace ClientCommon
{
    public sealed class IdSeg : TypeNameContainer<IdSeg>
    {
        /// <summary>
        /// 无效ID标记
        /// </summary>
        public const int InvalidId = 0;

        public static void Initialize()
        {
            if (initialized)
                return;

            SetTextSectionName("TypeDef_IdSeg");

            RegisterType("InvalidId", InvalidId, "InvalidId");

            initialized = true;
        }
    }
}
