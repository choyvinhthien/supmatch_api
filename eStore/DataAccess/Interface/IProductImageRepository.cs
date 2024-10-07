using eStore.Helpers;
using eStore.Models;

namespace eStore.DataAccess.Interface
{
    public interface IProductImageRepository
    {
        Task<List<ProductImageModel>> GetAllProductImages();
        Task<List<ProductImageModel>> GetProductImageByProductId(int productId);
        Task<ProductImageModel> GetProductImageByImageId(int imageId);
        Task AddProductImage(ProductModel productModel, UploadImage uploadImage);
        Task DeleteProductImage(int id);
        Task UpdateProductImage(int id, ProductImageModel productImageModel);
    }
}
