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
                    //Convert to Binary
                    string path = Path.Combine(@"D:\C#\Web API with ASP.NET Core\eStore\Uploads", productImageModel.ImageName);
                    byte[] fileBytes;
                    using (FileStream fileStream = System.IO.File.Create(path))
                    {
                        imageFile.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        fileBytes = new byte[fs.Length];
                        fs.Read(fileBytes, 0, Convert.ToInt32(fs.Length));
                    }

                    // File Oject
                    productImageModel.ImageData = fileBytes;
                    productImageModel.ImageSize = fileBytes.Length;
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
