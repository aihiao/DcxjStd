using System;
using System.Data;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LywGames;

namespace ClientCommon
{
    /// <summary>
    /// 数据表对应加载模块的基类, 用于统一处理数据库类的数据获取操作
    /// </summary>
    public sealed class DbClassLoader : AutoCreateSingleton<DbClassLoader>
    {
        private Dictionary<Type, ClassDesc> clsDescDic = new Dictionary<Type, ClassDesc>();

        public ClassDesc GetClassDesc(Type type)
        {
            ClassDesc clsDesc = null;
            if (clsDescDic.TryGetValue(type, out clsDesc))
            {
                return clsDesc;
            }

            clsDesc = ClassDesc.CreateClassDesc(type, this);
            clsDescDic.Add(type, clsDesc);

            return clsDesc;
        }

        public void ReleaseAll()
        {
            clsDescDic.Clear();
        }

        public void Release<T>(IDbAccessorFactory dbAcsFty)
        {
            if (clsDescDic.ContainsKey(typeof(T)))
            {
                dbAcsFty.Release(clsDescDic[typeof(T)].TableName);
                clsDescDic.Remove(typeof(T));
            }
        }

        /// <summary>
        /// 根据索引获取一条数据
        /// </summary>
        public T QueryData<T>(IDbAccessorFactory dbAcsFty, object key) where T : class, new()
        {
            try
            {
                var clsDesc = GetClassDesc(typeof(T));
                var dbAccessor = dbAcsFty.GetDbAccessor(clsDesc.TableName);

                IDataReader dbReader = null;
                if (clsDesc.KeyAttribute == null)
                {
                    dbReader = dbAccessor.Query(GetQueryString(clsDesc.TableName, "", null));
                }
                else
                {
                    object localKey = key;
                    if (clsDesc.KeyAttribute.CustomType != null && localKey != null)
                    {
                        localKey = ConfigDataBase.Instance.CustomDbClass.ParseText(clsDesc.KeyAttribute.CustomType, localKey);
                    }
                    dbReader = dbAccessor.Query(GetQueryString(clsDesc.TableName, clsDesc.KeyAttribute.ColumnName, localKey));
                }
                Dictionary<Type, IList> subItemDic = null;
                T result = ReadOneItem<T>(dbReader, clsDesc, ref subItemDic, dbAcsFty, key);
                dbAccessor.CloseDbReader();
                return result;
            }
            catch (Exception e)
            {
                var sb = new StringBuilder();
                sb.AppendLine(e.Message);
                sb.AppendLine(e.StackTrace);
                Debug.LogError(sb.ToString());
                return null;
            }
        }

        /// <summary>
        /// 获取整个表中的所有数据
        /// </summary>
        public List<T> QueryAllData<T>(IDbAccessorFactory dbAcsFty) where T : class, new()
        {
            try
            {
                ClassDesc clsDesc = GetClassDesc(typeof(T));
                IDbAccessor dbAccessor = dbAcsFty.GetDbAccessor(clsDesc.TableName);
                IDataReader reader = dbAccessor.Query(GetQueryString(clsDesc.TableName, clsDesc.KeyAttribute.ColumnName, null));
                Dictionary<Type, IList> subItemDic = null;
                List<T> result = ReadItems<T>(reader, clsDesc, ref subItemDic, dbAcsFty, null);
                dbAccessor.CloseDbReader();
                return result;
            }
            catch (Exception e)
            {
                var sb = new StringBuilder();
                sb.AppendLine(e.Message);
                sb.AppendLine(e.StackTrace);
                Debug.LogError(sb.ToString());
                return new List<T>();
            }
        }

        /// <summary>
		/// 解析一条数据
		/// </summary>
        private T ReadOneItem<T>(IDataReader dr, ClassDesc clsDesc, ref Dictionary<Type, IList> subItemDic, IDbAccessorFactory dbAcsFty, object key) where T : class, new()
        {
            while(dr.Read())
            {
                return CreateOneItem(typeof(T), dr, clsDesc, ref subItemDic, dbAcsFty, key) as T;
            }
            return null;
        }

        /// <summary>
		/// 解析所有数据
		/// </summary>
        private List<T> ReadItems<T>(IDataReader dr, ClassDesc clsDesc, ref Dictionary<Type, IList> subItemDic, IDbAccessorFactory dbAcsFty, object key) where T : class, new()
        {
            List<T> list = new List<T>();
            while (dr.Read())
            {
                list.Add(CreateOneItem(typeof(T), dr, clsDesc, ref subItemDic, dbAcsFty, key) as T);
            }
            return list;
        }

        /// <summary>
		/// 解析所有数据
		/// </summary>
        private IList ReadItems(Type type, IDataReader dr, ClassDesc clsDesc, ref Dictionary<Type, IList> subItemDic, IDbAccessorFactory dbAcsFty, object key)
        {
            IList list = Activator.CreateInstance(typeof(List<>).MakeGenericType(type)) as IList;
            while (dr.Read())
            {
                list.Add(CreateOneItem(type, dr, clsDesc, ref subItemDic, dbAcsFty, key));
            }
            return list;
        }

        /// <summary>
		/// 从数据流获取数据类, 支持外键
		/// </summary>
        private object CreateOneItem(Type type, IDataReader dr, ClassDesc clsDesc, ref Dictionary<Type, IList> subItemDic, IDbAccessorFactory dbAcsFty, object key)
        {
            // 创建具体类型
            var model = Activator.CreateInstance(type);
            object localKey = null;
            // 填充表数据
            for (int i = 0; i < dr.FieldCount; i++)
            {
                var propertyDesc = clsDesc.GetPropertyInfo(dr.GetName(i));
                if (propertyDesc != null)
                {
                    if (clsDesc.KeyAttribute != null && propertyDesc.attribute.ColumnName.Equals(clsDesc.KeyAttribute.ColumnName))
                    {
                        localKey = dr[i];
                    }

                    if (dr[i] is DBNull)
                    {
                        Debug.Log(string.Format("DBNull found in column : {0}.If it's test record, ignore", dr.GetName(i)));
                    }
                    else if (propertyDesc.attribute.CustomType == null)
                    {
                        if (propertyDesc.attribute.IsFloatCol)
                        {
                            int ori = Convert.ToInt32(dr[i]);
                            float dst = (float)ori / 10000;
                            propertyDesc.propertyInfo.SetValue(model, dst, null);
                        }
                        else
                        {
                            // 设置非自定义值
                            // bool值特殊处理
                            if (propertyDesc.propertyInfo.PropertyType == typeof(bool))
                            {
                                propertyDesc.propertyInfo.SetValue(model, Convert.ToInt32(dr[i]) == 1, null);
                            }
                            else if (propertyDesc.propertyInfo.PropertyType == typeof(int))
                            {
                                propertyDesc.propertyInfo.SetValue(model, Convert.ToInt32(dr[i]), null);
                            }
                            else
                            {
                                if (propertyDesc.propertyInfo.PropertyType == typeof(string))
                                {
                                    string value = dr[i] as string;
                                    propertyDesc.propertyInfo.SetValue(model, value, null);
                                }
                                else
                                {
                                    propertyDesc.propertyInfo.SetValue(model, dr[i], null);
                                }
                            }
                        }
                    }
                    else
                    {
                        // 对于自定义类型值, 使用CustomDBClass转换
                        if (dr[i].GetType() != typeof(string))
                        {
                            Debug.LogError(string.Format("Column value of custom type must be string : {0}", dr.GetName(i)));
                        }
                        else
                        {
                            var value = ConfigDataBase.Instance.CustomDbClass.ParseType(propertyDesc.attribute.CustomType, dr[i] as string);
                            propertyDesc.propertyInfo.SetValue(model, value, null);
                        }
                    }
                }
                else
                {
                    var splitPropertyInfo = clsDesc.GetSplitPropertyInfo(dr.GetName(i));
                    var splitRangeInfo = clsDesc.GetSplitRangePropertyInfo(dr.GetName(i));
                    if (splitPropertyInfo != null)
                    {
                        if (dr[i] is DBNull || dr.GetValue(i) == null)
                        {
                            Debug.Log(string.Format("DBNull found in column : {0}. If it's test record, ignore", dr.GetName(i)));
                        }
                        else
                        {
                            AddSplitColumn(splitPropertyInfo.splitAttribute.Type, splitPropertyInfo.propertyInfo, dr.GetValue(i).ToString(), splitPropertyInfo.splitAttribute.IsCustomType, model);
                        }
                    }
                    else if (splitRangeInfo != null)
                    {
                        if (dr[i] is DBNull || dr.GetValue(i) == null)
                        {
                            Debug.Log(string.Format("DBNull found in column : {0}.If it's test record, ignore", dr.GetName(i)));
                        }
                        else
                        {
                            AddSplitRangeColumn(splitRangeInfo.propertyInfo, dr.GetValue(i).ToString(), model);
                        }
                    }
                    else
                    {
                        int dstPrefixLen = 0;
                        string className = string.Empty;
                        if (string.IsNullOrEmpty(clsDesc.ExpectClassName))
                        {
                            className = clsDesc.ClassName + "List";
                            dstPrefixLen = clsDesc.TableName.Length;// 如此，则合并表不能有editor等类似的前缀
                        }
                        else // 有to_name注释
                        {   // 需要根据className 去 获得mergeColumnDesc
                            className = clsDesc.ExpectClassName + "List";
                            dstPrefixLen = clsDesc.ToNameStr.Length;
                        }
                        var mergeColumnDesc = clsDesc.GetMergePropertyInfo(className);
                        if (mergeColumnDesc != null)
                        {
                            PropertyInfo[] piArr = mergeColumnDesc.mergeAttribute.Type.GetProperties();
                            DbColumnAttribute secondAtb = piArr[1].GetCustomAttributes(typeof(DbColumnAttribute), false)[0] as DbColumnAttribute;
                            string dstName = secondAtb.ColumnName;

                            int mergeColCount = piArr.Length;
                            string colName = dr.GetName(i);
                            int index = colName.IndexOf('_');
                            if (index != -1 && dr.FieldCount >= i + mergeColCount - 1)
                            {
                                string prefix = colName.Substring(0, index);
                                dstName = prefix + dstName.Substring(dstPrefixLen);
                                if (colName.Equals(dstName, StringComparison.CurrentCultureIgnoreCase))
                                {
                                    List<string> valueList = new List<string>();
                                    valueList.Add(prefix.ToUpper());
                                    valueList.Add(i.ToString());

                                    for (int j = 1; j < mergeColCount - 1; j++)
                                    {
                                        colName = dr.GetName(i + j);
                                        DbColumnAttribute atb = piArr[j + 1].GetCustomAttributes(typeof(DbColumnAttribute), false)[0] as DbColumnAttribute;
                                        dstName = atb.ColumnName;
                                        dstName = prefix + dstName.Substring(dstPrefixLen);
                                        if (!colName.Equals(dstName, StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            throw new InvalidOperationException("DBClassLoader found MergeTable column format error");
                                        }
                                        else
                                        {
                                            valueList.Add((j + i).ToString());
                                        }
                                    }

                                    AddMergeColumn(mergeColumnDesc.mergeAttribute.Type, mergeColumnDesc.propertyInfo, model, valueList, dr);
                                    i += mergeColCount - 2;
                                }
                                else
                                {
                                    addNonMergeColumn(mergeColumnDesc.mergeAttribute.Type, dr, clsDesc, ref model, i);
                                }
                            }
                            else
                            {
                                addNonMergeColumn(mergeColumnDesc.mergeAttribute.Type, dr, clsDesc, ref model, i);
                            }
                        }
                    }
                }
            }

            // 填充子表数据
            for (int i = 0; i < clsDesc.SubTablePropertyDescList.Count; i++)
            {
                var subTablePropertyDesc = clsDesc.SubTablePropertyDescList[i];
                // 外键属性都应该是List<>类型
                var list = subTablePropertyDesc.propertyInfo.GetValue(model, null) as IList;
                // 根据具List<>对应的泛型类型获取数据
                var subItemList = GetSubItemList(ref subItemDic, subTablePropertyDesc.attribute.ClassType, dbAcsFty, localKey);
                // 将于model匹配的数据添加到对应的外键属性
                AddSubItems2ReferenceList(subItemList, GetClassDesc(subTablePropertyDesc.attribute.ClassType), list, model);
            }

            return model;
        }

        private void addNonMergeColumn(Type type, IDataReader dr, ClassDesc clsDesc, ref object model, int idx)
        {
            var propertyDesc = clsDesc.GetPropertyInfo(dr.GetName(idx));
            if (propertyDesc != null)
            {
                if (dr[idx] is DBNull)
                {
                    Debug.Log(string.Format("DBNull found in column : {0}.If it's a test record, ignore", dr.GetName(idx)));
                }
                else if (propertyDesc.attribute.CustomType == null)
                {
                    // 设置非自定义值
                    // bool值特殊处理
                    if (propertyDesc.propertyInfo.PropertyType == typeof(bool))
                    {
                        propertyDesc.propertyInfo.SetValue(model, Convert.ToInt32(dr[idx]) == 1, null);
                    }
                    else
                    {
                        if (propertyDesc.propertyInfo.PropertyType == typeof(string))
                        {
                            string value = dr[idx] as string;
                            propertyDesc.propertyInfo.SetValue(model, value, null);
                        }
                        else
                        {
                            propertyDesc.propertyInfo.SetValue(model, dr[idx], null);
                        }
                    }
                }
                else
                {
                    // 对于自定义类型值, 使用CustomDBClass转换
                    if (dr[idx].GetType() != typeof(string))
                    {
                        Debug.LogError(string.Format("Column value of custom type must be string : {0}", dr.GetName(idx)));
                    }
                    else
                    {
                        var value = ConfigDataBase.Instance.CustomDbClass.ParseType(propertyDesc.attribute.CustomType, dr[idx] as string);
                        propertyDesc.propertyInfo.SetValue(model, value, null);
                    }
                }
            }
        }

        private IList GetSplitItemList(Type type, string valueStr, bool isCustomType, ref IList list)
        {
            SplitColumnValueParser parser = new SplitColumnValueParser();
            List<string> valueList = parser.splitMultiValue2List(valueStr);
            if (isCustomType)
            {
                foreach (string value in valueList)
                {
                    var item = Activator.CreateInstance(type);
                    List<string> valFieldList = parser.splitDiffElement2List(value);
                    PropertyInfo[] piArr = type.GetProperties();
                    if (valFieldList.Count != piArr.Length)
                    {
                        Debug.Log(string.Format("splitColumn value count {0} property {1} is not match", valFieldList.Count, piArr.Length));
                    }

                    foreach (PropertyInfo pi in piArr)
                    {
                        var fieldAtbArr = pi.GetCustomAttributes(typeof(DbSplitFieldAttribute), false);
                        if (fieldAtbArr.Length != 0)
                        {
                            var fieldAtb = fieldAtbArr[0] as DbSplitFieldAttribute;
                            // 拆3项只填了1;2;这样的话，会拆成 1  2 ""  isHasData = true
                            // 若是 1;2，会拆成 1 2 isHasData = false
                            // 若填了1; 会拆成 1 ""  isHasData = false
                            // 故，统一将数据不完整的情况设置为false
                            string fieldValue = ""; // 如果需要拆分n项，而只填了n-1或者更少的项，则认为fieldValue是""
                            bool isHasData = (fieldAtb.Index <= valFieldList.Count);
                            if (isHasData)
                            {
                                fieldValue = valFieldList[fieldAtb.Index - 1];
                                if (fieldValue == "")
                                {
                                    isHasData = false; // 如果解析出来的是""，仍然认为其数据为默认值
                                }
                            }

                            if (fieldAtb.IsCustomType)
                            {
                                if (fieldAtb.Type.IsPrimitive)
                                {
                                    // 这是list
                                    if (isHasData) // 有数据的才进行这些处理，对应字符串段没填的话，就用默认值
                                    {
                                        List<string> subValList = parser.splitMultiValue2List(fieldValue);
                                        var subList = pi.GetValue(item, null) as IList;
                                        foreach (var subVal in subValList)
                                        {
                                            if (fieldAtb.Type == typeof(int))
                                            {
                                                if (!string.IsNullOrEmpty(subVal)) // 如果解析出空串，说明列表为空
                                                {
                                                    subList.Add(Convert.ToInt32(subVal));
                                                }
                                            }
                                            else if (fieldAtb.Type == typeof(string))
                                            {
                                                subList.Add(subVal);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    AddSplitColumn(fieldAtb.Type, pi, fieldValue, true, item);
                                }
                            }
                            else
                            {
                                if (isHasData) // 有数据才回写, 不然就是默认值
                                {
                                    // 设置非自定义值
                                    // bool值特殊处理
                                    if (pi.PropertyType == typeof(bool))
                                    {
                                        pi.SetValue(item, Convert.ToInt32(fieldValue) == 1, null);
                                    }
                                    else if (pi.PropertyType == typeof(int))
                                    {
                                        pi.SetValue(item, Convert.ToInt32(fieldValue), null);
                                    }
                                    else
                                    {
                                        if (pi.PropertyType == typeof(string))
                                        {
                                            pi.SetValue(item, fieldValue, null);
                                        }
                                        else
                                        {
                                            pi.SetValue(item, fieldValue, null);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    list.Add(item);
                }
            }
            else
            {
                if (type == typeof(int))
                {
                    foreach (string value in valueList)
                    {
                        try
                        {
                            list.Add(Convert.ToInt32(value));
                        }
                        catch (Exception e)
                        {
                            Debug.LogWarning(string.Format("GetSplitItemList type {0} valueStr {1}  value{3} Convert.ToInt32 found exception {2}", type, valueStr, e.ToString(), value));
                        }
                    }
                }
                else
                {
                    foreach (string value in valueList)
                    {
                        list.Add(value);
                    }
                }
            }

            return list;
        }

        private void AddSplitRangeColumn(PropertyInfo pi, string valueStr, object model)
        {
            IList list = pi.GetValue(model, null) as IList;
            List<int> rangeList = SplitColumnValueParser.splitRangeElement(valueStr);
            if (rangeList == null)
            {
                Debug.LogWarning(string.Format("AddSplitRangeColumn value {0} parse error", valueStr));
            }
            foreach (var item in rangeList)
            {
                list.Add(item);
            }
        }

        private void AddSplitColumn(Type type, PropertyInfo pi, string valueStr, bool isCustomType, object model)
        {
            // 外键属性都应该是List<>类型
            IList list = pi.GetValue(model, null) as IList;
            // 根据具List<>对应的泛型类型获取数据
            GetSplitItemList(type, valueStr, isCustomType, ref list);
        }

        private void AddMergeColumn(Type type, PropertyInfo pi, object model, List<string> valueList, IDataReader dr)
        {
            IList list = pi.GetValue(model, null) as IList;
            var item = Activator.CreateInstance(type);

            PropertyInfo[] piArr = type.GetProperties();
            if (piArr.Length != valueList.Count)
            {
                // warn
            }

            for (int i = 0; i < piArr.Length && i < valueList.Count; i++)
            {
                string fieldValue = valueList[i];
                PropertyInfo pInfo = piArr[i];
                var atbArr = pInfo.GetCustomAttributes(typeof(DbColumnAttribute), false);
                if (atbArr.Length != 0)
                {
                    var atb = atbArr[0] as DbColumnAttribute;
                    if (atb.CustomType == null)
                    {
                        // 设置非自定义值
                        // bool值特殊处理
                        if (pInfo.PropertyType == typeof(bool))
                        {
                            if (i == 0)
                            {
                                pInfo.SetValue(item, Convert.ToInt32(fieldValue) == 1, null);
                            }
                            else
                            {
                                if (dr[Convert.ToInt32(fieldValue)] is DBNull)
                                {
                                    Debug.Log(string.Format("DBNull found in column : {0}.  If it's a test record, ignore ", dr.GetName(Convert.ToInt32(fieldValue))));
                                }
                                else
                                {
                                    bool value = dr.GetInt32(Convert.ToInt32(fieldValue)) == 1;
                                    pInfo.SetValue(item, value, null);
                                }
                            }
                        }
                        else if (pInfo.PropertyType == typeof(int))
                        {
                            if (i == 0)
                            {
                                pInfo.SetValue(item, Convert.ToInt32(fieldValue), null);
                            }
                            else
                            {
                                if (dr[Convert.ToInt32(fieldValue)] is DBNull)
                                {
                                    Debug.Log(string.Format("DBNull found in column : {0}.  If it's a test record, ignore ", dr.GetName(Convert.ToInt32(fieldValue))));
                                }
                                else
                                {
                                    int value = dr.GetInt32(Convert.ToInt32(fieldValue));
                                    pInfo.SetValue(item, value, null);
                                }
                            }
                        }
                        else if (atb.IsFloatCol)
                        {
                            if (dr[Convert.ToInt32(fieldValue)] is DBNull)
                            {
                                Debug.Log(string.Format("DBNull found in column : {0}.  If it's a test record, ignore ", dr.GetName(Convert.ToInt32(fieldValue))));
                            }
                            else
                            {
                                int ori = dr.GetInt32(Convert.ToInt32(fieldValue));
                                float dst = (float)ori / 10000;
                                pInfo.SetValue(item, dst, null);
                            }
                        }
                        else
                        {
                            //合并表中不会存在“0”字符串
                            if (i == 0)
                            {
                                pInfo.SetValue(item, fieldValue, null);
                            }
                            else
                            {
                                if (dr[Convert.ToInt32(fieldValue)] is DBNull)
                                {
                                    Debug.Log(string.Format("DBNull found in column : {0}.  If it's a test record, ignore ", dr.GetName(Convert.ToInt32(fieldValue))));
                                }
                                else
                                {
                                    string value = dr.GetString(Convert.ToInt32(fieldValue));
                                    pInfo.SetValue(item, value, null);
                                }
                            }
                        }
                    }
                    else
                    {
                        var value = ConfigDataBase.Instance.CustomDbClass.ParseType(atb.CustomType, fieldValue);
                        pInfo.SetValue(item, value, null);
                    }
                }
            }

            list.Add(item);
        }

        /// <summary>
        /// 从已加载的数据中获取对应类型的List<>, 如果对应类型不存在, 加载并返回加载后的结果
        /// </summary>
        private IList GetSubItemList(ref Dictionary<Type, IList> subItemDic, Type type, IDbAccessorFactory dbAcsFty, object key)
        {
            ClassDesc clsDesc = GetClassDesc(type);
            IDbAccessor dbAccessor = dbAcsFty.GetDbAccessor(clsDesc.TableName);
            IDataReader reader = dbAccessor.Query(GetQueryString(clsDesc.TableName, clsDesc.RootKeyAttribute.ColumnName, key));
            IList list = ReadItems(type, reader, clsDesc, ref subItemDic, dbAcsFty, key);
            dbAccessor.CloseDbReader();
            return list;
        }

        /// <summary>
        /// 从数据组中将于referenceObj对应的实体填充到referenceList
        /// </summary>
        private static void AddSubItems2ReferenceList(IList list, ClassDesc clsDesc, IList refList, object refObj)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var obj = list[i];
#if UNITY_IPHONE || UNITY_IOS
#else
                if (IsMatchReferenceClass(clsDesc, obj, refObj))
#endif
                {
                    refList.Add(obj);
                }
            }
        }

        /// <summary>
        /// 判断一个外键类型是否与其相应的referenceObj匹配, 也就是外键值全都一致
        /// </summary>
        private static bool IsMatchReferenceClass(ClassDesc clsDesc, object obj, object refObj)
        {
            for (int i = 0; i < clsDesc.FkPropertyInfoList.Count; i++)
            {
                var fkPropertyDesc = clsDesc.FkPropertyInfoList[i];
                if (fkPropertyDesc.propertyInfo.GetValue(obj, null).Equals(fkPropertyDesc.referencedPropertyInfo.GetValue(refObj, null)) == false)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
		/// 获取数据库请求字符串, 如果keyValue为空, 获取所有的
		/// </summary>
        private string GetQueryString(string tableName, string keyColumn, object keyValue)
        {
            if (keyValue != null)
            {
                if (keyValue.GetType().Equals(typeof(string)))
                {
                    string kv = keyValue as string;
                    kv = kv.Replace("\'", "\\'");
                    return string.Format("select * from {0} where {1}=\'{2}\'", tableName, keyColumn, kv);
                }
                else
                {
                    return string.Format("select * from {0} where {1}={2}", tableName, keyColumn, keyValue);
                }
            }
            else
            {
                return string.Format("select * from {0}", tableName);
            }
        }

    }
}
