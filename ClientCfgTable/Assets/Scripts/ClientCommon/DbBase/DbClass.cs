using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
    /// <summary>
    /// 标记数据库表对应类的属性, 记录表名和类名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DbTableAttribute : Attribute
    {
        /// <summary>
        /// 类对应的数据库表名
        /// </summary>
        private string tableName = string.Empty;
        public string TableName { get { return tableName; } }

        // to_name中定义的名字
        private string expectClassName = string.Empty;
        public string ExpectClassName { get { return expectClassName; } }

        /// <summary>
        /// 类名, 由于混淆, 不能用type.Name
        /// </summary>
        private string className = string.Empty;
        public string ClassName { get { return className; } }

        /// <summary>
        /// 数据库中to_name注释, 为了不引入复杂的转换规则, 故加入该注解
        /// </summary>
        private string toNameStr = string.Empty;
        public string ToNameStr { get { return toNameStr; } }

        public DbTableAttribute(string tableName, string className = "", string expectClassName = "", string toNameStr = "")
        {
            this.tableName = tableName;
            this.className = className;
            this.expectClassName = expectClassName;
            this.toNameStr = toNameStr;
        }
    }

    /// <summary>
    /// 标记数据库表中列拆分后对应的类的属性, 记录表名和数据库列名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class DbSubColumnClassAttribute : Attribute
    {
        /// <summary>
        /// 类对应的数据库表名
        /// </summary>
        private string tableName = string.Empty;
        private string TableName { get { return tableName; } }

        /// <summary>
        /// 对应的数据库列名
        /// </summary>
        private string columnName = string.Empty;
        public string ColumnName { get { return columnName; } }

        public DbSubColumnClassAttribute(string tableName, string columnName)
        {
            this.tableName = tableName;
            this.columnName = columnName;
        }
    }

    /// <summary>
    /// 标记数据库表中列对应的字段的属性, 记录数据库列的属性(数据库列名、是否是主键、是否是外键等)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DbColumnAttribute : Attribute
    {
        /// <summary>
        /// 是否是主键
        /// </summary>
        private bool keyColumn = false;
        public bool KeyColumn { get { return keyColumn; } }

        /// <summary>
        /// 对应的数据库列名
        /// </summary>
        private string columnName = string.Empty;
        public string ColumnName { get { return columnName; } }

        /// <summary>
        /// 自定义类型
        /// </summary>
        private Type customType = null;
        public Type CustomType { get { return customType; } }

        /// <summary>
        /// 是否为int转float列
        /// </summary>
        private bool isFloatCol = false;
        public bool IsFloatCol { get { return isFloatCol; } }

        /// <summary>
        /// 是否是外键
        /// </summary>
        private bool isForeignKey = false;
        public bool IsForeignKey { get { return isForeignKey; } }

        /// <summary>
        /// 外键引用的表名
        /// </summary>
        private Type referencedClass = null;
        public Type ReferencedClass { get { return referencedClass; } }

        /// <summary>
        /// 外键引用的列名
        /// </summary>
        private string referencedColumn = string.Empty;
        public string ReferencedColumn { get { return referencedColumn; } }

        public DbColumnAttribute(bool keyColumn, string columnName, Type customType = null, bool isFloatCol = false)
        {
            this.keyColumn = keyColumn;
            this.columnName = columnName;
            this.customType = customType;
            this.isFloatCol = isFloatCol;
        }

        public DbColumnAttribute(bool keyColumn, string columnName, bool isForeignKey, Type referencedClass, string referencedColumn, Type customType = null, bool isFloatCol = false)
        {
            this.keyColumn = keyColumn;
            this.columnName = columnName;
            this.customType = customType;
            this.isFloatCol = isFloatCol;
            this.isForeignKey = isForeignKey;
            this.referencedClass = referencedClass;
            this.referencedColumn = referencedColumn;
        }
        
    }

    /// <summary>
    /// 用于标记数据库表中可拆分列对应的字段属性, 记录数据库列名和拆分后类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DbSplitColumnAttribute : Attribute
    {
        /// <summary>
        /// 数据库列名
        /// </summary>
        private string columnName = string.Empty;
        public string ColumnName { get { return columnName; } }

        /// <summary>
        /// 数据库列拆分后可解析成的类型
        /// </summary>
        private Type type = null;
        public Type Type { get { return type; } }

        /// <summary>
        /// 是否是自定义类型(自定义类)或者基础类型(int, string, float, double)
        /// </summary>
        private bool isCustomType = false;
        public bool IsCustomType { get { return isCustomType; } }

        public DbSplitColumnAttribute(Type type, string columnName, bool isCustomType)
        {
            this.columnName = columnName;
            this.type = type;
            this.isCustomType = isCustomType;
        }
    }

    /// <summary>
    /// 用于标记数据库表中可拆分成范围的列(例如: 1~35)对应的字段属性, 记录数据库列名
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DbSplitRangeAttribute : Attribute
    {
        /// <summary>
        /// 数据库列名
        /// </summary>
        private string columnName = string.Empty;
        public string ColumnName { get { return columnName; } }

        public DbSplitRangeAttribute(string columnName)
        {
            this.columnName = columnName;
        }
    }

    /// <summary>
    /// 用于标记可以合并多个数据库表中列的字段属性, 记录合并后的类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DbMergeColumnAttribute : Attribute
    {
        /// <summary>
        /// 合并后的类型
        /// </summary>
        private Type type = null;
        public Type Type { get { return type; } }

        /// <summary>
        /// 该注解对应的字段名
        /// </summary>
        private string fieldTypeName = string.Empty;
        public string FieldTypeName { get { return fieldTypeName; } }

        public DbMergeColumnAttribute(Type type, string fieldTypeName)
        {
            this.type = type;
            this.fieldTypeName = fieldTypeName;
        }
    }

    /// <summary>
    /// 标记数据库表中列拆分后对应的类的字段的属性, 记录第几个字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DbSplitFieldAttribute : Attribute
    {
        /// <summary>
        /// 第几个字段, index-1当下标使用
        /// </summary>
        private int index = 0;
        public int Index { get { return index; } }

        /// <summary>
        /// 是否是自定义类型, 太复杂了, 几乎不使用
        /// </summary>
        private bool isCustomType = false;
        public bool IsCustomType { get { return isCustomType; } }

        private Type type = null;
        public Type Type { get { return type; } }

        public DbSplitFieldAttribute(int index, bool isCustomType = false, Type type = null)
        {
            this.index = index;
            this.isCustomType = isCustomType;
            this.type = type;
        }
    }

    /// <summary>
    /// 标记有外键链接的属性, 一般为List<>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class DbColumnSubTableAttribute : Attribute
    {
        /// <summary>
        /// 实际对应的数据类型
        /// </summary>
        private Type classType = null;
        public Type ClassType { get { return classType; } }

        /// <summary>
        /// 对应的数据库子表名称
        /// </summary>
        private string subTableName = string.Empty;
        public string SubTableName { get { return subTableName; } }

        public DbColumnSubTableAttribute(Type classType, string subTableName)
        {
            this.classType = classType;
            this.subTableName = subTableName;
        }
    }

    /// <summary>
    /// 数据类的描述, 从对应的类解析并保存数据相关的类型
    /// </summary>
    public sealed class ClassDesc
    {
        /// <summary>
        /// 保存成员属性与外键相关的信息
        /// </summary>
        public sealed class ForeignKeyPropertyDesc
        {
            public DbColumnAttribute attribute;
            public PropertyInfo propertyInfo;
            public PropertyInfo referencedPropertyInfo;
        }

        public sealed class PropertyDesc
        {
            public DbColumnAttribute attribute;
            public PropertyInfo propertyInfo;
        }

        public sealed class SplitPropertyDesc
        {
            public DbSplitColumnAttribute splitAttribute;
            public PropertyInfo propertyInfo;
        }

        public sealed class SplitRangePropertyDesc
        {
            public DbSplitRangeAttribute rangeAttribute;
            public PropertyInfo propertyInfo;
        }

        public sealed class MergePropertyDesc
        {
            public DbMergeColumnAttribute mergeAttribute;
            public PropertyInfo propertyInfo;
        }

        /// <summary>
        /// 保存子表属性相关信息
        /// </summary>
        public sealed class SubTablePropertyDesc
        {
            public DbColumnSubTableAttribute attribute;
            public PropertyInfo propertyInfo;
        }

        /// <summary>
        /// 对应的数据类
        /// </summary>
        private Type classType = null;
        public Type ClassType { get { return classType; } }

        /// <summary>
        /// 对应的数据库表名
        /// </summary>
        private string tableName = string.Empty;
        public string TableName { get { return tableName; } }

        private string expectClassName = string.Empty;
        public string ExpectClassName { get { return expectClassName; } }

        /// <summary>
        /// 存储类名, 由于混淆, 原type.Name不能用
        /// </summary>
        private string className = string.Empty;
        public string ClassName { get { return className; } }

        /// <summary>
        /// 数据库表中toName的注释
        /// </summary>
        private string toNameStr = string.Empty;
        public string ToNameStr { get { return toNameStr; } }

        /// <summary>
        /// 索引列的名称
        /// </summary>
        private DbColumnAttribute keyAttribute = null;
        public DbColumnAttribute KeyAttribute { get { return keyAttribute; } }

        /// <summary>
        /// 字表中与根表主键相对应的属性
        /// </summary>
        private DbColumnAttribute rootKeyAttribute = null;
        public DbColumnAttribute RootKeyAttribute { get { return rootKeyAttribute; } }

        /// <summary>
        /// 表中定义数据列对应的属性信息
        /// </summary>
        private Dictionary<string, PropertyDesc> propertyInfoDic = new Dictionary<string, PropertyDesc>();
        public Dictionary<string, PropertyDesc> PropertyInfoDic { get { return propertyInfoDic; } }

        /// <summary>
        /// 表中定义拆分列对应的属性信息
        /// </summary>
        private Dictionary<string, SplitPropertyDesc> splitPropertyInfoDic = new Dictionary<string, SplitPropertyDesc>();
        public Dictionary<string, SplitPropertyDesc> SplitPropertyInfoDic { get { return splitPropertyInfoDic; } }

        private Dictionary<string, SplitRangePropertyDesc> splitRangePropertyInfoDic = new Dictionary<string, SplitRangePropertyDesc>();
        public Dictionary<string, SplitRangePropertyDesc> SplitRangePropertyInfoDic { get { return splitRangePropertyInfoDic; } }

        private Dictionary<string, MergePropertyDesc> mergePropertyInfoDic = new Dictionary<string, MergePropertyDesc>();
        public Dictionary<string, MergePropertyDesc> MergePropertyInfoDic { get { return mergePropertyInfoDic; } }

        /// <summary>
        /// 外键对应所引用属性的信息
        /// </summary>
        private List<ForeignKeyPropertyDesc> fkPropertyInfoList = new List<ForeignKeyPropertyDesc>();
        public List<ForeignKeyPropertyDesc> FkPropertyInfoList { get { return fkPropertyInfoList; } }

        /// <summary>
        /// 子表属性对应的信息
        /// </summary>
        private List<SubTablePropertyDesc> subTablePropertyDescList = new List<SubTablePropertyDesc>();
        public List<SubTablePropertyDesc> SubTablePropertyDescList { get { return subTablePropertyDescList; } }

        /// <summary>
        /// 通过列名获取对应的属性信息
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public PropertyDesc GetPropertyInfo(string columnName)
        {
            return propertyInfoDic.ContainsKey(columnName) ? propertyInfoDic[columnName] : null;
        }

        public SplitPropertyDesc GetSplitPropertyInfo(string columnName)
        {
            return splitPropertyInfoDic.ContainsKey(columnName) ? splitPropertyInfoDic[columnName] : null;
        }

        public SplitRangePropertyDesc GetSplitRangePropertyInfo(string columnName)
        {
            return splitRangePropertyInfoDic.ContainsKey(columnName) ? splitRangePropertyInfoDic[columnName] : null;
        }

        public MergePropertyDesc GetMergePropertyInfo(string name)
        {
            return mergePropertyInfoDic.ContainsKey(name) ? mergePropertyInfoDic[name] : null;
        }

        /// <summary>
        /// 从数据库类创建描述类
        /// </summary>
        /// <param name="type"></param>
        /// <param name="clsLoader"></param>
        /// <returns></returns>
        public static ClassDesc CreateClassDesc(Type type, DbClassLoader dbClsLoader)
        {
            ClassDesc clsDesc = new ClassDesc();
            clsDesc.classType = type;

            if (!type.IsDefined(typeof(DbTableAttribute), false))
            {
                // 数据库类需要定义DbTableAttribute
                Debug.LogError(string.Format("Missing table name {0}", type.ToString()));
            }
            else
            {
                // 获取表名
                var atbAyy = type.GetCustomAttributes(typeof(DbTableAttribute), false);
                if (atbAyy.Length != 0)
                {
                    var atb = atbAyy[0] as DbTableAttribute;
                    clsDesc.tableName = atb.TableName;
                    clsDesc.expectClassName = atb.ExpectClassName;
                    clsDesc.className = atb.ClassName;
                    clsDesc.toNameStr = atb.ToNameStr;
                }
            }

            // 分析属性, 同时需要分析基类的属性
            var currentType = type;
            while (currentType != null && currentType != typeof(object))
            {
                CollectPropertyInfo(currentType.GetProperties(), clsDesc, dbClsLoader);
                currentType = currentType.BaseType;
            }

            return clsDesc;
        }

        private static void CollectPropertyInfo(PropertyInfo[] piArr, ClassDesc clsDesc, DbClassLoader dbClsLoader)
        {
            for (int i = 0; i < piArr.Length; i++)
            {
                PropertyInfo pi = piArr[i];

                object[] atbArr = pi.GetCustomAttributes(typeof(DbColumnAttribute), false);
                object[] splitAtbArr = pi.GetCustomAttributes(typeof(DbSplitColumnAttribute), false);
                object[] splitRangeAtbArr = pi.GetCustomAttributes(typeof(DbSplitRangeAttribute), false);
                object[] mergeAtbArr = pi.GetCustomAttributes(typeof(DbMergeColumnAttribute), false);
                if (atbArr.Length != 0)
                {
                    // 检测和获取主键列信息
                    var atb = atbArr[0] as DbColumnAttribute;
                    if (atb.KeyColumn)
                    {
                        if (atb.IsForeignKey)
                        {
                            throw new Exception("Pk can't be foreign key.");
                        }

                        // 主键一定不是外键
                        if (clsDesc.keyAttribute != null)
                        {
                            Debug.LogError(string.Format("Key column already exist {0}", atb.ColumnName));
                        }
                        else
                        {
                            clsDesc.keyAttribute = atb;
                        }
                    }

                    if (atb.IsForeignKey)
                    {
                        // 保存外键信息
                        var foreignKeyPropertyDesc = new ForeignKeyPropertyDesc();
                        foreignKeyPropertyDesc.attribute = atb;
                        foreignKeyPropertyDesc.propertyInfo = pi;

                        // 对应的ReferencedPropertyInfo
                        var refClsDesc = dbClsLoader.GetClassDesc(atb.ReferencedClass);
                        foreignKeyPropertyDesc.referencedPropertyInfo = refClsDesc.GetPropertyInfo(atb.ReferencedColumn).propertyInfo;

                        clsDesc.rootKeyAttribute = atb;
                        clsDesc.fkPropertyInfoList.Add(foreignKeyPropertyDesc);
                    }

                    // 保存属性信息
                    if (clsDesc.propertyInfoDic.ContainsKey(atb.ColumnName))
                    {
                        Debug.LogWarning(string.Format("Duplicated column name {0}", atb.ColumnName));
                    }
                    else
                    {
                        var propertyDesc = new PropertyDesc();
                        propertyDesc.attribute = atb;
                        propertyDesc.propertyInfo = pi;
                        clsDesc.propertyInfoDic.Add(atb.ColumnName, propertyDesc);
                    }
                    continue;
                }
                else if (splitAtbArr.Length != 0)
                {
                    // 检测和获取主键列信息
                    var splitAtb = splitAtbArr[0] as DbSplitColumnAttribute;

                    if (clsDesc.splitPropertyInfoDic.ContainsKey(splitAtb.ColumnName))
                    {
                        Debug.LogWarning(string.Format("Duplicated column name {0}", splitAtb.ColumnName));
                    }
                    else
                    {
                        var propertyDesc = new SplitPropertyDesc();
                        propertyDesc.splitAttribute = splitAtb;
                        propertyDesc.propertyInfo = pi;
                        clsDesc.splitPropertyInfoDic.Add(splitAtb.ColumnName, propertyDesc);
                    }
                    continue;
                }
                else if (mergeAtbArr.Length != 0)
                {
                    var mergeAtb = mergeAtbArr[0] as DbMergeColumnAttribute;
                    if (clsDesc.mergePropertyInfoDic.ContainsKey(mergeAtb.FieldTypeName))
                    {
                        Debug.LogWarning(string.Format("Duplicated column name {0}", mergeAtb.FieldTypeName));
                    }
                    else
                    {
                        var propertyDesc = new MergePropertyDesc();
                        propertyDesc.mergeAttribute = mergeAtb;
                        propertyDesc.propertyInfo = pi;
                        clsDesc.mergePropertyInfoDic.Add(mergeAtb.FieldTypeName, propertyDesc);
                    }
                    continue;
                }
                else if (splitRangeAtbArr.Length != 0)
                {
                    var splitRangeAtb = splitRangeAtbArr[0] as DbSplitRangeAttribute;
                    if (clsDesc.splitRangePropertyInfoDic.ContainsKey(splitRangeAtb.ColumnName))
                    {
                        Debug.LogWarning(string.Format("Duplicated column name {0}", splitRangeAtb.ColumnName));
                    }
                    else
                    {
                        var propertyDesc = new SplitRangePropertyDesc();
                        propertyDesc.rangeAttribute = splitRangeAtb;
                        propertyDesc.propertyInfo = pi;
                        clsDesc.splitRangePropertyInfoDic.Add(splitRangeAtb.ColumnName, propertyDesc);
                    }
                }

                // 获取子表信息
                atbArr = pi.GetCustomAttributes(typeof(DbColumnSubTableAttribute), false);
                if (atbArr.Length != 0)
                {
                    var atb = atbArr[0] as DbColumnSubTableAttribute;

                    var subTablePropertyDesc = new SubTablePropertyDesc();
                    subTablePropertyDesc.attribute = atb;
                    subTablePropertyDesc.propertyInfo = pi;
                    clsDesc.subTablePropertyDescList.Add(subTablePropertyDesc);

                    continue;
                }
            }
        }

    }

}
