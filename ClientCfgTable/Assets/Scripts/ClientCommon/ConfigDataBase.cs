using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LywGames;

namespace ClientCommon
{
    public partial class ConfigDataBase : AutoCreateSingleton<ConfigDataBase>
    {
        bool inited = false;

        bool constructedLocalDb = false;
        public bool ConstructedLocalDb
        {
            get { return constructedLocalDb; }
        }

        bool constructedLocalDbError = false;
        public bool ConstructedLocalDbError
        {
            get { return constructedLocalDbError; }
        }

        private IDbAccessorFactory dbAccessorFactory = null;
        public IDbAccessorFactory DbAccessorFactory
        {
            get { return dbAccessorFactory; }
        }
        public void SetAccessorFactory(IDbAccessorFactory factory)
        {
            dbAccessorFactory = factory;
        }

        private CustomDbClass customDbClass = null;
        public CustomDbClass CustomDbClass
        {
            get { return customDbClass; }
        }

        private string defaultPath = string.Empty;
        private Dictionary<string, string> configDbPathDic = new Dictionary<string, string>();

        private Dictionary<Type, Configuration> configDic = new Dictionary<Type, Configuration>();

        private DateTime lastReleaseTime = DateTime.Now;
        private readonly TimeSpan ReleaseDelta = new TimeSpan(0, 0, 300);

        public void Initialize(IDbAccessorFactory dbAccessorFactory, string subPath)
        {
            if (inited)
            {
                return;
            }
            inited = true;

            defaultPath = FileManager.GetPersistentPath(subPath);

            ConstructConfigDbPath();
            SetAccessorFactory(dbAccessorFactory);
            if (customDbClass == null)
            {
                customDbClass = new CustomDbClass();
            }
            RegisterEnumerationType(customDbClass);
            ConstValue.Initialize();

            customDbClass.RegisterTypeParser(typeof(LywColor), LywColor.Parse);
            customDbClass.RegisterTypeParser(typeof(LywVector2), LywVector2.Parse);
            customDbClass.RegisterTypeParser(typeof(LywVector3), LywVector3.Parse);
            customDbClass.RegisterTypeParser(typeof(LywRect), LywRect.Parse);
        }

        void OnUnpackFileFinished(bool success)
        {
            constructedLocalDb = true;
            if (!success)
            {
                constructedLocalDbError = true;
            }
        }

        public IEnumerator ConstructLocalConfigDbFile(string subPath, MonoBehaviour coroutineMono)
        {
            List<string> filePathList = new List<string>();
            foreach (var tbName in dbNameDic.Keys)
            {
                string tableName = string.Format(@"{0}.{1}", ConfigDataBase.Instance.GetDbNameByTableName(tbName), Defines.ConfigFileExtension);
                filePathList.Add(tableName);
            }
            constructedLocalDb = false;
            yield return GameShellManager.Instance.StartCoroutine(UnpackFile2PersistentPath.ConstructLocalConfigDbFile(subPath, Defines.ConfigVersionFileName, true, filePathList, false, OnUnpackFileFinished, coroutineMono));
            yield return null;
        }

        private void ConstructConfigDbPath()
        {
            foreach (var tbName in dbNameDic.Keys)
            {
                if (!configDbPathDic.ContainsKey(tbName))
                {
                    configDbPathDic.Add(tbName, defaultPath);
                }
            }
        }

        public string GetTbPath(string tbName)
        {
            return configDbPathDic.ContainsKey(tbName) ? configDbPathDic[tbName] : defaultPath;
        }

        public T GetConfiguration<T>() where T : Configuration, new()
        {
            if (DateTime.Now - lastReleaseTime > ReleaseDelta)
            {
                foreach (var kvp in configDic)
                {
                    if (kvp.Key != typeof(T))
                    {
                        kvp.Value.ReleaseData(false);
                    }
                }
                lastReleaseTime = DateTime.Now;
            }

            Configuration configuration = null;
            if (configDic.TryGetValue(typeof(T), out configuration))
            {
                return configuration as T;
            }

            configuration = new T();
            configDic.Add(typeof(T), configuration);

            return configuration as T;
        }

        public void ReleaseAll(bool isForce)
        {
            foreach (var kvp in configDic)
            {
                kvp.Value.ReleaseData(isForce);
            }

            if (isForce)
            {
                configDic.Clear();
                DbClassLoader.Instance.ReleaseAll(); 
                dbAccessorFactory.ReleaseAll(); 
            }
        }

        public void Release<T>(bool isForce) where T : Configuration, new()
        {
            if (configDic.ContainsKey(typeof(T)))
            {
                configDic[typeof(T)].ReleaseData(isForce);

                if (isForce)
                {
                    configDic.Remove(typeof(T));
                }
            }
        }

        public string GetDbNameByTableName(string tableName)
        {
            return dbNameDic.ContainsKey(tableName) ? dbNameDic[tableName] : string.Empty;
        }

    }
}
