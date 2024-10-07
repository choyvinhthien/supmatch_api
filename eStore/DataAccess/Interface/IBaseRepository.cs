namespace eStore.DataAccess.Interface
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> FindAll();
    }
}
