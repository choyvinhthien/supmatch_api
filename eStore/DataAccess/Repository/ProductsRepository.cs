using eStore.DataAccess.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using eStore.Models;
using eStore.Helpers;
using Microsoft.CodeAnalysis;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;

namespace eStore.DataAccess.Repository
{
    public class ProductsRepository : BaseRepository<Product>, IProductsRepository
    {
        private readonly eStoreContext _context;
        private readonly IMapper _mapper;

        public ProductsRepository (eStoreContext context, IMapper mapper) :base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PagedList<Product>> GetProductsWithPaging(PagingParameters pagingParameters)
        {
            var products = PagedList<Product>.GetPagedList(FindAll().OrderBy(p => p.ProductId), pagingParameters.PageNumber, pagingParameters.PageSize);
            return products;
        }
        public async Task<List<ProductModel>> GetAllProducts()
        {
            var products = await _context.Products!.Include(p => p.ProductImages).Include(p => p.Category).ToListAsync();
            return _mapper.Map<List<ProductModel>>(products);
        }
        public async Task<ProductModel> GetProduct(int productId)
        {
            var product = await _context.Products!.Include(product => product.ProductImages).Include(product => product.Feedbacks).ThenInclude(feedback => feedback.User).SingleOrDefaultAsync(product => product.ProductId == productId);
            return _mapper.Map<ProductModel>(product);
        }
        public async Task AddProduct(ProductModel product)
        {
            var newProduct = _mapper.Map<Product>(product);
            _context.Products!.Add(newProduct);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProduct(int productId)
        {
            var deleteProduct = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == productId);
            if (deleteProduct != null)
            {
                _context.ProductImages!.RemoveRange(_context.ProductImages!.Where(productImage => productImage.ProductId == productId).ToList());
                _context.Products!.Remove(deleteProduct);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateProduct(ProductModel product)
        {
            var updateProduct = _mapper.Map<Product>(product);
            _context.Products.Update(updateProduct);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateListProducts(List<ProductModel> products)
        {
            var updateProducts = _mapper.Map<List<Product>>(products);

            foreach (var product in updateProducts)
            {
                // Detach the product from the context to prevent tracking
                _context.Entry(product).State = EntityState.Detached;
            }

            var removedCartItems = new List<CartItem>();
            foreach (var product in products)
            {
                var cartItemsToRemove = await _context.CartItems!
                                        .AsNoTracking()
                                        .Where(cartItem => cartItem.ProductId == product.ProductId)
                                        .ToListAsync();
                removedCartItems.AddRange(cartItemsToRemove);
            }

            foreach (var cartItem in removedCartItems)
            {
                // Detach the cart item from the context to prevent tracking
                _context.Entry(cartItem).State = EntityState.Detached;
            }

            _context.CartItems.RemoveRange(removedCartItems);
            _context.Products.UpdateRange(updateProducts);
            await _context.SaveChangesAsync();
        }
        public async Task<List<ProductModel>> GetSearchProducts(string searchProductName)
        {
            var searchProducts = await _context.Products.Where(p => p.ProductName.Contains(searchProductName)).ToListAsync();
            return _mapper.Map<List<ProductModel>>(searchProducts);
        }
        public async Task<PagedList<Product>> GetSearchProductsWithPaging(string searchProductName, PagingParameters pagingParameters)
        {
            var products = PagedList<Product>.GetPagedList(FindAll().Where(p => p.ProductName.Contains(searchProductName)).OrderBy(p => p.ProductId), pagingParameters.PageNumber, pagingParameters.PageSize);
            return products;
        }
        public async Task<ProductModel> GetNewestProduct()
        {
            var products = await _context.Products!.Include(p => p.ProductImages).ToListAsync();
            Product product = products.MaxBy(p => p.ReleasedDate);
            return _mapper.Map<ProductModel>(product);
        }
        public async Task<List<ProductModel>> GetFilterProducts(int minFilter, int maxFilter, string? rateFilter, string? categoryFilter)   
        {
            var products = new List<Product>();
            if (rateFilter.IsNullOrEmpty() && categoryFilter.IsNullOrEmpty())
            {
                products = await _context.Products!.Include(p => p.ProductImages).Include(p => p.Category).Include(p => p.Feedbacks).Where(p => p.UnitPrice >= minFilter && p.UnitPrice <= maxFilter).ToListAsync();
                return _mapper.Map<List<ProductModel>>(products);
            }
            else if (categoryFilter.IsNullOrEmpty()) 
            {
                var rateFilterArray =(rateFilter.Split(','));
                foreach( var item in rateFilterArray)
                {
                    products.AddRange(await _context.Products!.Include(p => p.ProductImages).Include(p => p.Category).Include(p => p.Feedbacks).Where(p => p.UnitPrice >= minFilter && p.UnitPrice <= maxFilter && p.ratingAverage == int.Parse(item)).ToListAsync());
                }
                return _mapper.Map<List<ProductModel>>(products.OrderBy(p=> p.ReleasedDate));
            }else if (rateFilter.IsNullOrEmpty())
            {
                var categoryFilterArray = (categoryFilter.Split(','));
                foreach (var item in categoryFilterArray)
                {
                    products.AddRange(await _context.Products!.Include(p => p.ProductImages).Include(p => p.Category).Include(p => p.Feedbacks).Where(p => p.UnitPrice >= minFilter && p.UnitPrice <= maxFilter && p.CategoryId == item).ToListAsync());
                }
                return _mapper.Map<List<ProductModel>>(products.OrderBy(p => p.ReleasedDate));
            }
            else
            {
                var categoryFilterArray = (categoryFilter.Split(','));
                var rateFilterArray = (rateFilter.Split(','));
                foreach (var item in categoryFilterArray)
                {
                    products.AddRange(await _context.Products!.Include(p => p.ProductImages).Include(p => p.Category).Include(p => p.Feedbacks).Where(p => p.UnitPrice >= minFilter && p.UnitPrice <= maxFilter && p.CategoryId == item && p.ratingAverage == int.Parse(item)).ToListAsync());
                }
                return _mapper.Map<List<ProductModel>>(products.OrderBy(p => p.ReleasedDate));
            }
        }
        public async Task<List<ProductModel>> GetRandomProducts()
        {
            int count = 10;
            // Count of rows in the table (optional, can be removed if you don't need it)
            int totalProducts = _context.Products.Count();

            if (totalProducts < count)
            {
                // Handle the case where there are fewer products than requested
                count = totalProducts;
            }

            // Generate a random skip value within the valid range
            int randomSkip = new Random().Next(0, totalProducts - count + 1);

            var products = _context.Products.Include(product => product.Category).Include(product => product.ProductImages)
                           .OrderBy(p => Guid.NewGuid()) // Randomize order
                           .Skip(randomSkip) // Skip random number of products
                           .Take(count) // Take the desired number of products
                           .ToList();
            return _mapper.Map<List<ProductModel>>(products);
        }
        public async Task UpdateRatingAverage(int productId)
        {
            var feedbacks = await context.Feedbacks.Where(feedback => feedback.ProductId == productId).ToListAsync();
            var product = await _context.Products!.AsNoTracking().SingleOrDefaultAsync(product => product.ProductId == productId);
            product.ratingAverage = (int)feedbacks.Average(feedback => feedback.Rating);
            _context.Products!.Update(product);
            await _context.SaveChangesAsync();
        }
        //-------------------------------------------------
        private async Task<bool> ProductExists(int id)
        {
            var product = await GetProduct(id);
            return product != null;
        }
        private int GenerateUniqueRandomProductId()
        {
            Random rnd = new Random(1);

            int id;
            do
            {
                id = rnd.Next(1, 9999);
            } while (ProductExists(id).Result);

            return id;
        }
    }
}
