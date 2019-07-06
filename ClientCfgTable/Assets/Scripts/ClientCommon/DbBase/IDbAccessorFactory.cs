namespace ClientCommon
{
    public interface IDbAccessorFactory
    {
        IDbAccessor GetDbAccessor(string tableName);

        void ReleaseAll();
    }
}
