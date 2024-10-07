using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eStore.DataAccess;
using eStore.DataAccess.Interface;
using eStore.Models;
using Microsoft.AspNetCore.Authorization;
using eStore.Helpers;
using Microsoft.AspNetCore.Cors;

namespace eStore.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IProductImageRepository productImageRepository;
        private readonly IValidation _validation;
        public ProductsController(IProductsRepository productsRepository, IProductImageRepository productImageRepository, IValidation validation)
        {
            _productsRepository = productsRepository;
            this.productImageRepository = productImageRepository;
            _validation = validation;
        }
        [HttpGet("GetAllProductImages")]
        /*[Authorize(Roles = AppRole.Customer)]*/
        public async Task<IActionResult> GetAllProductImages()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            try
            {
                return Ok(await productImageRepository.GetAllProductImages());
            }
            catch
            {
                return BadRequest();
            }
        }
        // GET: api/Products
        [HttpGet("GetAllProducts")]
        /*[Authorize(Roles = AppRole.Customer)]*/
        public async Task<IActionResult> GetAllProducts()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            try
            {
                return Ok(await _productsRepository.GetAllProducts());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("SearchProductByName")]
        /*[Authorize(Roles = AppRole.Customer)]*/
        public async Task<IActionResult> SearchProducts(string? search, [FromForm] PagingParameters pagingParameters)
        {
            try
            {
                if (search == null || search.Length == 0 || search == "")
                {
                    return Ok(await _productsRepository.GetProductsWithPaging(pagingParameters));
                }
                else
                {
                    return Ok(await _productsRepository.GetSearchProductsWithPaging(search, pagingParameters));
                }
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetNewestProduct")]
        public async Task<IActionResult> GetNewestProduct()
        {
            var product = await _productsRepository.GetNewestProduct();
            return product == null ? NotFound() : Ok(product);
        }
        // GET: api/Products/5
        [HttpGet("GetProductById/{id}")]
        /*[Authorize(Roles = AppRole.Customer)]*/
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productsRepository.GetProduct(id);
            return product == null ? NotFound() : Ok(product);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("UpdateProduct")]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> UpdateProduct(int id, string ProductName, string Description, decimal UnitPrice, int UnitsInstock)
        {
            try
            {
                ProductModel originalProduct = await _productsRepository.GetProduct(id);
                ProductModel product = new ProductModel
                {
                    ProductId = id,
                    ProductName = ProductName,
                    Description = Description,
                    UnitPrice = UnitPrice,
                    UnitsInstock = UnitsInstock,
                    ReleasedDate = originalProduct.ReleasedDate,
                    Status = originalProduct.Status
                };
                await _productsRepository.UpdateProduct(product);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddProduct")]
        /*[Authorize(Roles = AppRole.Customer)]*/
        public async Task<IActionResult> AddNewProduct([FromForm] frmAddProduct newProduct, [FromForm] UploadImage uploadImage)
        {
            ProductModel productModel = new ProductModel
            {
                ProductId = await _validation.GenerateUniqueId("Product"),
                ProductName = newProduct.ProductName,
                Description = newProduct.Description,
                CategoryId = newProduct.CategoryId,
                UnitPrice = newProduct.UnitPrice,
                UnitsInstock = newProduct.UnitsInstock,
                ReleasedDate = DateTime.UtcNow,
                Status = "Active"
            };
            try
            {
                await _productsRepository.AddProduct(productModel);
                await productImageRepository.AddProductImage(productModel, uploadImage);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("DeleteProduct/{id}")]
        /*[Authorize(Roles = AppRole.Customer)]*/
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productsRepository.DeleteProduct(id);
            return Ok();
        }
        [HttpPost("GetFilterProducts")]
        public async Task<IActionResult> GetFilterProducts([FromForm] frmFilterProduct filterProduct)
        {
            try
            {
                return Ok(await _productsRepository.GetFilterProducts(filterProduct.minFilter, filterProduct.maxFilter, filterProduct.rateFilter, filterProduct.categoryFilter));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("GetRandomProducts")]
        public async Task<IActionResult> GetRandomProducts()
        {
            try
            {
                return Ok(await _productsRepository.GetRandomProducts());
            }
            catch
            {
                return BadRequest();
            }
        }

        //--------------------------------------------------
        private async Task<bool> ProductExists(int id)
        {
            var product = await _productsRepository.GetProduct(id);
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
