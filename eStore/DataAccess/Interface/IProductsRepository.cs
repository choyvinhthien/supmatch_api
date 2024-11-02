using eStore.Helpers;
using eStore.Models;

namespace eStore.DataAccess.Interface
{
    public interface IProductsRepository : IBaseRepository<Product>
    {
        Task<List<ProductModel>> GetAllProducts();
        Task<List<ProductModel>> GetSearchProducts(string searchProductName);
        Task<ProductModel> GetProduct(int id);
        Task AddProduct(ProductModel product);
        Task DeleteProduct(int id);
        Task UpdateProduct(ProductModel product);
        Task<PagedList<Product>> GetProductsWithPaging(PagingParameters pagingParameters);
        Task<PagedList<Product>> GetSearchProductsWithPaging(string searchProductName, PagingParameters pagingParameters);
        Task<ProductModel> GetNewestProduct();
        Task<List<ProductModel>> GetFilterProducts(int minFilter, int maxFilter, string? rateFilter, string? categoryFilter);
        Task<List<ProductModel>> GetRandomProducts();
        Task UpdateRatingAverage(int productId);
        Task UpdateListProducts(List<ProductModel> products);
        Task<List<ProductModel>> GetProductsByAccountId(string accountId);
    }
}
