using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Security;
using System.Collections.Generic;
using UnityEngine;
using Mono.Xml;

public abstract class SaveData
{
    #region 常量
    protected static readonly string extension = ".xml";

    public const string SAVE_DATA_PARENT_FOLDER = "LocalData";
    #endregion  常量

    #region 成员变量
    //保存的文件名 实例化类时设置
    protected string folderName;    //文件夹名  是账号名 每个账号一个文件夹
    protected string fileName;  //文件名	和账号同名表示存在默认字典里 数据较少和不容易分类的数据放这

    protected string folderPath
    {
        get
        {
#if UNITY_EDITOR
            return Application.persistentDataPath + "/" + SAVE_DATA_PARENT_FOLDER + "/" + folderName;
            //return Application.streamingAssetsPath + "/" + folderName;
#else
        return Application.persistentDataPath + "/" + SAVE_DATA_PARENT_FOLDER + "/" + folderName;
#endif
        }
    }
    protected string fullPath
    {
        get
        {
#if UNITY_EDITOR
            return Application.persistentDataPath + "/" + SAVE_DATA_PARENT_FOLDER + "/" + folderName + "/" + fileName + extension;
#else
        return Application.persistentDataPath + "/" + SAVE_DATA_PARENT_FOLDER + "/"  + folderName + "/" + fileName + extension;
#endif
        }
    }
    //存数据 xml格式：string为ID，list中是具体属性 最终需要把数据转成这个格式才能存
    protected Dictionary<string, Dictionary<string, string>> data = new Dictionary<string, Dictionary<string, string>>();
    #endregion  成员变量

    #region 功能函数
    //传文件名，不包括扩展名 文件名是必须有的，所以不要定义没参数的构造函数，避免忘了设置名字
    public SaveData()
    {
        fileName = DataModelManager.Instance == null ? "default" : DataModelManager.Instance.RoleId.ToString();
        folderName = DataModelManager.Instance == null ? "default" : DataModelManager.Instance.LoginInfo.LastAreaId.ToString() + "_" + DataModelManager.Instance.RoleId.ToString();
    }

    #endregion  功能函数

    #region 需要子类实现的方法
    protected abstract Dictionary<string, string> getXmlPairs(); //将数据设置成key="value"
    protected abstract void setXmlData();   //设置为<tag Name="Steve" Level="10" ... />
    protected abstract void analysisXml();  //将xml字符串解析成自定义数据结构
    protected abstract void setDefaultData();   //xml解析失败时 设置默认值
    #endregion 需要子类实现的方法

    #region 加载和保存本地文件
    //从指定路径加载xml 返回指定格式数据 具体解析放在子类里
    public void GetXmlData()
    {
        _getXmlData(fullPath);
    }

    private void _getXmlData(string path)
    {
        if (!Directory.Exists(folderPath))
        {

            // TODO ,检查非法路径
            Directory.CreateDirectory(folderPath);
        }
        if (!File.Exists(path))
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                fs.Close();
                setDefaultData();
            }
            return;
        }
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            string str = new UTF8Encoding().GetString(bytes);
            SecurityParser sp = new SecurityParser();
            try
            {
                sp.LoadXml(str);
                SecurityElement se = sp.ToXml();
                data = new Dictionary<string, Dictionary<string, string>>();
                foreach (SecurityElement child in se.Children)
                {
                    Hashtable elements = child.Attributes;
                    Dictionary<string, string> temp = new Dictionary<string, string>();
                    foreach (object key in elements.Keys)
                    {
                        temp.Add(key.ToString(), child.Attributes[key].ToString());
                    }
                    data.Add(child.Tag, temp);
                }
                analysisXml();
            }
            catch (Exception)
            {
                setDefaultData();
            }
        }
    }

    //保存到文件
    public void Save()  //格式<tag Name="Steve" Level="10" ... />
    {
        setXmlData();
        _save(fullPath);
    }

    private void _save(string path)
    {
        if (data == null)
        {
            Debug.LogError("current path:" + path + "data is null");
            return;
        }
        var xmlParser = new SecurityParser();
        SmallXmlParser.AttrListImpl attrList = new SmallXmlParser.AttrListImpl();
        xmlParser.OnStartElement("Datas", null);//只是个标记 没实际作用
        foreach (var item in data)
        {
            attrList.Clear();
            foreach (var container in item.Value)
            {
                attrList.Add(container.Key, container.Value);
            }
            xmlParser.OnStartElement(item.Key, attrList);
            xmlParser.OnEndElement(item.Key);
        }
        xmlParser.OnEndElement("Datas");
        using (var fs = File.Exists(path) ? new FileStream(path, FileMode.Truncate) : new FileStream(path, FileMode.Create))
        {
            byte[] content = new UTF8Encoding().GetBytes(xmlParser.ToXml().ToString());
            fs.Write(content, 0, content.Length);
            fs.Flush();
            fs.Close();
        }
    }
    //TODO 需要整理  先完成功能
    public void SaveToFile(string fileName, Dictionary<string, Dictionary<string, string>> data)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        var xmlParser = new SecurityParser();
        SmallXmlParser.AttrListImpl attrList = new SmallXmlParser.AttrListImpl();
        xmlParser.OnStartElement("Datas", null);//只是个标记 没实际作用
        foreach (var item in data)
        {
            attrList.Clear();
            foreach (var container in item.Value)
            {
                attrList.Add(container.Key, container.Value);
            }
            xmlParser.OnStartElement(item.Key, attrList);
            xmlParser.OnEndElement(item.Key);
        }
        xmlParser.OnEndElement("Datas");
        string path = folderPath + "/" + fileName + extension;
        using (var fs = File.Exists(path) ? new FileStream(path, FileMode.Truncate) : new FileStream(path, FileMode.Create))
        {
            byte[] content = new UTF8Encoding().GetBytes(xmlParser.ToXml().ToString());
            fs.Write(content, 0, content.Length);
            fs.Flush();
            fs.Close();
        }
    }

    public bool CheckFileExit(string name)
    {
        if (!Directory.Exists(folderPath))
            return false;
        string path = folderPath + "/" + name + extension;
        return File.Exists(path);
    }

    //读取游戏系统数据文件
    public Dictionary<string, Dictionary<string, string>> ReadFile(string fileName)
    {
        Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
        string path = folderPath + "/" + fileName + extension;
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            string str = new UTF8Encoding().GetString(bytes);
            SecurityParser sp = new SecurityParser();
            try
            {
                sp.LoadXml(str);
                SecurityElement se = sp.ToXml();
                foreach (SecurityElement child in se.Children)
                {
                    Hashtable elements = child.Attributes;
                    Dictionary<string, string> temp = new Dictionary<string, string>();
                    foreach (object key in elements.Keys)
                    {
                        temp.Add(key.ToString(), child.Attributes[key].ToString());
                    }
                    result.Add(child.Tag, temp);
                }
            }
            catch (Exception)
            {
                setDefaultData();
            }
        }
        return result;
    }
    #endregion 加载和保存本地文件

}

