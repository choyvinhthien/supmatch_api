using eStore.DataAccess.Interface;
using AutoMapper;
using eStore.Models;
using Microsoft.CodeAnalysis;
using eStore.Helpers;
using Microsoft.EntityFrameworkCore;

namespace eStore.DataAccess.Repository
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly eStoreContext _context;
        private readonly IMapper _mapper;

        public ProductImageRepository(eStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductImageModel>> GetAllProductImages()
        {
            var productsImages= await _context.ProductImages!.ToListAsync();
            return _mapper.Map<List<ProductImageModel>>(productsImages);
        }
        public async Task<List<ProductImageModel>> GetProductImageByProductId(int productId)
        {
            var productImage = await _context.ProductImages!.Where(p => p.ProductId == productId).ToListAsync();
            return _mapper.Map<List<ProductImageModel>>(productImage);
        }
        public async Task<ProductImageModel> GetProductImageByImageId(int imageId)
        {
            var productImage = await _context.ProductImages!.FindAsync(imageId);
            return _mapper.Map<ProductImageModel>(productImage);
        }
        public async Task AddProductImage(ProductModel productModel, UploadImage uploadImage)
        {
            int i = 1;
            foreach (var imageFile in uploadImage.ImageFile)
            {
                ProductImageModel productImageModel = new ProductImageModel
                {
                    ImageId = GenerateUniqueRandomImageId(),
                    ProductId = productModel.ProductId,
                    ImageName = productModel.ProductId + "_" + productModel.ProductName + "_" + i +".png",
                };
                if (imageFile.Length > 0)
                {
                    // Chuyển đổi file ảnh thành dữ liệu nhị phân trực tiếp mà không cần lưu vào thư mục
                    using (var memoryStream = new MemoryStream())
                    {
                        imageFile.CopyTo(memoryStream);
                        productImageModel.ImageData = memoryStream.ToArray();
                        productImageModel.ImageSize = (int)memoryStream.Length;
                    }
                }
                //add to db
                var newProductImage = _mapper.Map<ProductImage>(productImageModel);
                _context.ProductImages!.Add(newProductImage);
                await _context.SaveChangesAsync();
                i++;
            }
        }

        public async Task DeleteProductImage(int productId)
        {
            var deleteProductImage = _context.ProductImages!.FirstOrDefault(p => p.ProductId == productId);
            if (deleteProductImage != null)
            {
                _context.ProductImages!.Remove(deleteProductImage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProductImage(int ProductId, ProductImageModel productImageModel)
        {
            if (ProductId == productImageModel.ProductId)
            {
                var updateProductImage = _mapper.Map<ProductImage>(productImageModel);
                _context.ProductImages!.Update(updateProductImage);
                await _context.SaveChangesAsync();
            }
        }
        //-------------------------------------------------
        private async Task<bool> ProductImageExists(int id)
        {
            var productImage = await GetProductImageByImageId(id);
            return productImage != null;
        }
        private int GenerateUniqueRandomImageId()
        {
            Random rnd = new Random(1);

            int id;
            do
            {
                id = rnd.Next(1, 9999);
            } while (ProductImageExists(id).Result);

            return id;
        }
    }
}
