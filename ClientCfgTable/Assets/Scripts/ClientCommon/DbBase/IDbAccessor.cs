using System.Data;

namespace ClientCommon
{
    public interface IDbAccessor
    {
        IDataReader Query(string query);

#if UNITY_EDITOR
        int ExecuteNonQuery(string sql);
#endif

        void CloseDbReader();

        void CloseDbConn();
    }
}
