using System;

public class UiRegister
{
    public static UiConfigData[] GetAllUiConfigDatas()
    {
        UiConfigData[] datas =
        {
        };

        return datas;
    }

    public struct UiConfigData
    {
        public Type type;
        public string prefabName;
        public Type[] linkedTypes;
        public bool hideOtherModules;
        public Type[] ignoreMutexTypes;

        public UiConfigData(Type type, string prefabName) : this(type, prefabName, null, false, null) { }
        public UiConfigData(Type type, string prefabName, Type[] linkedTypes) : this(type, prefabName, linkedTypes, false, null) { }
        public UiConfigData(Type type, string prefabName, Type[] linkedTypes, bool hideOtherModules, Type[] ignoreMutexTypes)
        {
            this.type = type;
            this.prefabName = prefabName;
            this.linkedTypes = linkedTypes;
            this.hideOtherModules = hideOtherModules;
            this.ignoreMutexTypes = ignoreMutexTypes;
        }
    }

}

