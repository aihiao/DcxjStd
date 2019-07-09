using System;
using System.Data;
using System.Threading;
using UnityEngine;
using Mono.Data.Sqlite;

namespace ClientCommon
{
    /// <summary>
    /// Sqlite存取器
    /// </summary>
    public class SqliteAccessor : IDbAccessor
    {
        private string dbPath;
        private IDbConnection dbConn;
        private IDataReader reader;

        public SqliteAccessor(string dbPath)
        {
            this.dbPath = dbPath;

            try
            {
                dbConn = new SqliteConnection(dbPath); 
                dbConn.Open();

#if UNITY_EDITOR
                ExecuteNonQuery("PRAGMA synchronous = OFF;");
#endif
            }
            catch (Exception e)
            {
                Debug.LogError(string.Format("SqliteAccessor new SqliteConnection {0} exception details {1}", dbPath, e.ToString()));
                throw e;
            }
        }

        public ConnectionState getConnState()
        {
            return dbConn.State;
        }

        public void OpenConn()
        {
            dbConn.Open();
        }

        public void CloseDbReader()
        {
            if (reader != null)
            {
                reader.Close();
                reader.Dispose();
                reader = null;
            }
        }

        public void CloseDbConn()
        {
            if (dbConn != null)
            {
                dbConn.Close();
                dbConn.Dispose();
                dbConn = null;
            }
        }

        public void ReallyCloseDb()
        {
#if UNITY_EDITOR
            ExecuteNonQuery("PRAGMA synchronous = FULL;");
#endif

            CloseDbReader();

            CloseDbConn();
        }

        public IDataReader Query(string query)
        {
            Monitor.Enter(dbPath);
            try
            {
                using (var dbCmd = dbConn.CreateCommand())
                {
                    dbCmd.CommandText = query;
                    reader = dbCmd.ExecuteReader();
                }

                return reader;
            }
            catch (Exception e)
            {
                Debug.LogError(string.Format("sqlite execute query: {0} failed details {1}", query, e.ToString()));
                throw e;
            }
            finally
            {
                Monitor.Exit(dbPath);
            }
        }

#if UNITY_EDITOR
        public int ExecuteNonQuery(string sql)
        {
            Monitor.Enter(dbPath);
            try
            {
                int result = 0;
                using (var dbCmd = dbConn.CreateCommand())
                {
                    dbCmd.CommandText = sql;
                    result = dbCmd.ExecuteNonQuery();
                }

                return result;
            }
            catch (Exception e)
            {
                Debug.LogError(string.Format("sqlite ExecuteNonQuery query: {0} failed details {1}", sql, e.ToString()));
                throw e;
            }
            finally
            {
                Monitor.Exit(dbPath);
            }
        }
#endif

    }
}