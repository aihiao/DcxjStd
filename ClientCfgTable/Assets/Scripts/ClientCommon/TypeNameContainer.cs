using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
    /// <summary>
    /// 所有需要解析的类型定义都需要从这个类派生
    /// </summary>
    /// <typeparam name="Type">>继承类的类型</typeparam>
    public class TypeNameContainer<Type>
    {
        protected static bool initialized = false;
        private static string textSectionName = "Enum_Block";

        private static List<int> typeList = new List<int>();
        private static Dictionary<string, KeyValuePair<int, string>> container = new Dictionary<string, KeyValuePair<int, string>>();

        /// <summary>
        /// 获取已注册类型值数量
        /// </summary>
        /// <returns></returns>
        public static int GetRegisterTypeCount()
        {
            return typeList.Count;
        }

        /// <summary>
        /// 获取已经注册的类型值
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public static int GetRegisterTypeByIndex(int idx)
        {
            return typeList[idx];
        }

        /// <summary>
        /// 判断一个类型名是否是有效的类型名
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static bool IsValidType(string typeName)
        {
            return container.ContainsKey(typeName);
        }

        /// <summary>
        /// 通过类型名获取类型值
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static int GetTypeByName(string typeName)
        {
            if (!IsValidType(typeName))
            {
                Debug.LogError(string.Format("Invalid Type {0} in {1}", typeName, typeof(Type)));

                return 0;
            }

            return container[typeName].Key;
        }

        /// <summary>
        /// 通过类型值获取类型名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetNameByType(object type)
        {
            foreach (KeyValuePair<string, KeyValuePair<int, string>> kvp in container)
            {
                if (kvp.Value.Key == Convert.ToInt32(type))
                {
                    return kvp.Key;
                }
            }

            Debug.LogError(string.Format("Invalid value {0} in {1}", type, typeof(Type)));
            return string.Empty;
        }

        /// <summary>
        /// 通过类型值获取用于显示的名称, 这个名字在对应的StringConfig中
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetDisplayNameByType(int type)
        {
            foreach (KeyValuePair<string, KeyValuePair<int, string>> kvp in container)
            {
                if (kvp.Value.Key == type)
                {
                    return kvp.Value.Value;
                }
            }

            Debug.LogError(string.Format("Invalid value {0} in {1}", type, typeof(Type)));
            return string.Empty;
        }

        /// <summary>
        /// 通过类型名解析类型值
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int Parse(string typeName, int defValue = 0)
        {
            if (string.IsNullOrEmpty(typeName) || (!IsValidType(typeName)))
            {
                Debug.LogError(string.Format("Invalid Type {0} in {1}", typeName, typeof(Type)));
                return defValue;
            }

            return GetTypeByName(typeName);
        }

        public static int ParseNoCase(string typeName, int defValue)
        {
            foreach (var kvp in container)
            {
                if ((!string.IsNullOrEmpty(typeName)) && kvp.Key.Equals(typeName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return kvp.Value.Key;
                }
            }

            return defValue;
        }

        /// <summary>
        /// 通过类型名列表, 解析类型值列表
        /// </summary>
        /// <param name="typeNameList"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static List<int> ParseList(string typeNameList, int defValue)
        {
            List<int> typeList = new List<int>();
            if (!string.IsNullOrEmpty(typeNameList))
            {
                string[] typeNameArr = typeNameList.Split(',');
                foreach (var typeName in typeNameArr)
                {
                    typeList.Add(Parse(typeName, defValue));
                }
            }

            return typeList;
        }

        /// <summary>
        /// 通过类型名列表, 解析类型值组合值
        /// </summary>
        /// <param name="typeNameList"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static int ParseBitList(string typeNameList, int defValue)
        {
            if (string.IsNullOrEmpty(typeNameList))
            {
                return defValue;
            }

            int types = 0;
            string[] typeNameArr = typeNameList.Split(',');
            foreach (var typeName in typeNameArr)
            {
                types |= Parse(typeName, defValue);
            }

            return types;
        }

        protected static void SetTextSectionName(string name)
        {
            textSectionName = name;
        }

        protected static bool RegisterType(string typeName, int type)
        {
            return RegisterType(typeName, type, typeName);
        }

        /// <summary>
        /// 注册类型名、值、显示名
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="type"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        protected static bool RegisterType(string typeName, int type, string displayName)
        {
            if (container.ContainsKey(typeName))
            {
                Debug.LogError(string.Format("Invalid Type {0} in {1}", typeName, typeof(Type)));
                return false;
            }

            typeList.Add(type);
            container.Add(typeName, new KeyValuePair<int, string>(type, displayName));

            return true;
        }

    }
}
