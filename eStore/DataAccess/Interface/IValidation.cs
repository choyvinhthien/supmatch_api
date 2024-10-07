namespace eStore.DataAccess.Interface
{
    public interface IValidation
    {
        Task<int> LastIdInTable(string tableName);
        Task<int> GenerateUniqueId(string tableName);

    }
}
