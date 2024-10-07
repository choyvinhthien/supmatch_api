using eStore.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace eStore.DataAccess.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected eStoreContext context {  get; set; }
        public BaseRepository(eStoreContext context)
        {
            this.context = context;
        }
        public IQueryable<T> FindAll()
        {
            return this.context.Set<T>().AsNoTracking();
        }
    }
}
