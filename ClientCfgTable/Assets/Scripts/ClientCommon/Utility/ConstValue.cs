using System;
using System.Data;
using UnityEngine;

namespace ClientCommon
{
    public partial class ConstValue
    {
        private static string GetValue(string name)
        {
            string result = string.Empty;
            try
            {
                IDbAccessor dbAccessor = ConfigDataBase.Instance.DbAccessorFactory.GetDbAccessor("const_value");
                IDataReader reader = dbAccessor.Query("select `value` from `const_value` where `key`='" + name + "'");

                if (reader.Read())
                {
                    result = reader.GetValue(0).ToString();
                }
                dbAccessor.CloseDbReader();
            }
            catch (Exception e)
            {
                Debug.LogError("ConstValue.GetValue found " + e.ToString() + " when read " + name);
            }

            return result;
        }
    }
}
