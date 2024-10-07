using eStore.Models;

namespace eStore.DataAccess.Interface
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetAllCategories();
    }
}
