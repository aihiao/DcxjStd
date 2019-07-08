namespace ClientCommon
{
    public interface IDbAccessorFactory
    {
        IDbAccessor GetDbAccessor(string tableName);

        void Release(string tableName);

        void ReleaseAll();
    }
}
