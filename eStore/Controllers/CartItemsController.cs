using eStore.DataAccess.Interface;
using eStore.DataAccess.Repository;
using eStore.Helpers;
using eStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        public readonly ICartItemRepository _cartItemRepository;
        public readonly IProductsRepository _productsRepository;
        public readonly IShoppingCartRepository _shoppingCartRepository;
        public readonly IValidation _validation;
        public CartItemsController(ICartItemRepository cartItemRepository, IProductsRepository productsRepository, IValidation validation, IShoppingCartRepository shoppingCartRepository)
        {
            _cartItemRepository = cartItemRepository;
            _productsRepository = productsRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _validation = validation;
        }
        [HttpPost("AddToCart")]
        /*[Authorize(Roles = AppRole.Customer)]*/
        public async Task<IActionResult> AddToCart([FromForm] frmAddToCart newCartItem)
        {
            CartItemModel availableCartItem = await _cartItemRepository.CheckAvailableCart(newCartItem.UserId, newCartItem.ProductId);
            if (availableCartItem!=null)
            {
                try
                {
                    var additionalQuantity = newCartItem.Quantity;
                    int maxQuantity = availableCartItem.Product.UnitsInstock;
                    CartItemModel cartItemModel = new CartItemModel
                    {
                        CartItemId = availableCartItem.CartItemId,
                        ShoppingCartId = availableCartItem.ShoppingCartId,
                        ProductId = availableCartItem.ProductId,
                        Quantity = availableCartItem.Quantity
                    };
                    await _cartItemRepository.AddQuantity(cartItemModel, additionalQuantity, maxQuantity);
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                CartItemModel cartItem = new CartItemModel
                {
                    CartItemId = await _validation.GenerateUniqueId("CartItem"),
                    ShoppingCartId = (await _shoppingCartRepository.GetShoppingCartByUserId(newCartItem.UserId)).ShoppingCartId,
                    ProductId = newCartItem.ProductId,
                    Quantity = newCartItem.Quantity
                };
                try
                {
                    await _cartItemRepository.AddToCart(cartItem);
                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
        }

        [HttpDelete("RemoveCartItem/{id}")]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> RemoveCartItem(int id)
        {
            await _cartItemRepository.RemoveCartItem(id);
            return Ok();
        }

        [HttpPut("AddQuantity")]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> AddQuantity([FromForm]frmCartItem frmCartItem,[FromForm] int additionalQuantity, [FromForm] int maxQuantity)
        {
            try
            {
                CartItemModel cartItemModel = new CartItemModel
                {
                    CartItemId = frmCartItem.CartItemId,
                    ShoppingCartId = frmCartItem.ShoppingCartId,
                    ProductId = frmCartItem.ProductId,
                    Quantity = frmCartItem.Quantity
                };
                await _cartItemRepository.AddQuantity(cartItemModel, additionalQuantity, maxQuantity);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
