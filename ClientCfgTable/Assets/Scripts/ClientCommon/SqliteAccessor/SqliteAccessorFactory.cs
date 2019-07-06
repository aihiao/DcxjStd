using System;
using System.Data;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

namespace ClientCommon
{
    public class SqliteAccessorFactory : IDbAccessorFactory
    {
        private static Dictionary<string, SqliteAccessor> sqlAccessorPool = new Dictionary<string, SqliteAccessor>();

        public SqliteAccessorFactory() { }

        public IDbAccessor GetDbAccessor(string tableName)
        {
            SqliteAccessor sqlAccessor = null;

            Monitor.Enter("GetDbAccessor");
            try
            {
                if (!sqlAccessorPool.ContainsKey(tableName))
                {
                    string dbPath = string.Format(@"URI=file:{0}/{1}.{2}", ConfigDataBase.Instance.GetTbPath(tableName), ConfigDataBase.Instance.GetDbNameByTableName(tableName), Defines.ConfigFileExtension);
                    sqlAccessorPool.Add(tableName, new SqliteAccessor(dbPath));
                }
                sqlAccessor = sqlAccessorPool[tableName];

                if (sqlAccessor.getConnState() != ConnectionState.Open)
                {
                    sqlAccessor.OpenConn();
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace + "\r\n" + e.Message);
            }
            finally
            {
                Monitor.Exit("GetDbAccessor");
            }

            return sqlAccessor;
        }

        public void ReleaseAll()
        {
            Monitor.Enter("ReleaseAll");
            try
            {
                List<string> tableNameList = new List<string>();
                foreach (var kvp in sqlAccessorPool)
                {
                    tableNameList.Add(kvp.Key);
                }

                foreach (var tableName in tableNameList)
                {
                    sqlAccessorPool[tableName].ReallyCloseDb();
                }

                sqlAccessorPool.Clear();
                SqliteConnection.ClearAllPools();
            }
            catch (Exception e)
            {
                Debug.LogError(e.StackTrace + "\r\n" + e.Message);
            }
            finally
            {
                Monitor.Exit("ReleaseAll");
            }
        }

    }
}
