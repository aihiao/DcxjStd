using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
    public class CustomDbClass 
    {
        public delegate object ParseTypeDelegate(string text);
        public delegate string ParseTextDelegate(object value);

        private Dictionary<Type, ParseTypeDelegate> typeParserDic = new Dictionary<Type, ParseTypeDelegate>();
        private Dictionary<Type, ParseTextDelegate> textParserDic = new Dictionary<Type, ParseTextDelegate>();

        public void RegisterTypeParser(Type type, ParseTypeDelegate del)
        {
            if (typeParserDic.ContainsKey(type))
            {
                Debug.LogWarning(string.Format("Duplicate CustomDbClass : {0}", type));
            }
            typeParserDic.Add(type, del);
        }

        public void RegisterTextParser(Type type, ParseTextDelegate del)
        {
            if (textParserDic.ContainsKey(type))
            {
                Debug.LogWarning(string.Format("Duplicate CustomDbClass : {0}", type));
            }
            textParserDic.Add(type, del);
        }

        public object ParseType(Type type, string text)
        {
            ParseTypeDelegate del = null;
            if (typeParserDic.TryGetValue(type, out del))
            {
                return del(text);
            }
            return null;
        }

        public string ParseText(Type type, object value)
        {
            ParseTextDelegate del = null;
            if (textParserDic.TryGetValue(type, out del))
            {
                return del(value);
            }
            return null;
        }

    }

    public class LywColor
    {
        public float r = 0;
        public float g = 0;
        public float b = 0;
        public float a = 0;

        public static LywColor Parse(string text)
        {
            LywColor result = new LywColor();

            string[] strArr = text.Split(',');
            if (strArr.Length > 4)
            {
                result.r = float.Parse(strArr[0]);
                result.g = float.Parse(strArr[1]);
                result.b = float.Parse(strArr[2]);
                result.a = float.Parse(strArr[3]);
            }

            return result;
        }

    }

    public class LywVector2
    {
        public float x = 0;
        public float y = 0;

        public static LywVector2 Parse(string text)
        {
            LywVector2 result = new LywVector2();

            string[] strArr = text.Split(',');
            if (strArr.Length > 2)
            {
                result.x = float.Parse(strArr[0]);
                result.y = float.Parse(strArr[1]);
            }

            return result;
        }

        public string ToXmlString()
        {
            return x + "," + y;
        }

    }

    public class LywVector3
    {
        public float x = 0;
        public float y = 0;
        public float z = 0;

        public static LywVector3 Parse(string text)
        {
            LywVector3 result = new LywVector3();

            string[] strArr = text.Split(',');
            if (strArr.Length > 3)
            {
                result.x = float.Parse(strArr[0]);
                result.y = float.Parse(strArr[1]);
                result.z = float.Parse(strArr[2]);
            }

            return result;
        }

        public string ToXmlString()
        {
            return x + "," + y + "," + z;
        }

    }

    public class LywRect
    {
        public float x = 0;
        public float y = 0;
        public float width = 0;
        public float height = 0;

        public static LywRect Parse(string text)
        {
            LywRect result = new LywRect();

            string[] strArr = text.Split(',');
            if (strArr.Length > 4)
            {
                result.x = float.Parse(strArr[0]);
                result.y = float.Parse(strArr[1]);
                result.width = float.Parse(strArr[2]);
                result.height = float.Parse(strArr[3]);
            }

            return result;
        }

    }

}