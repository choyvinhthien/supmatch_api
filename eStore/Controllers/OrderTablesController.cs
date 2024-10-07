using eStore.DataAccess;
using eStore.DataAccess.Interface;
using eStore.DataAccess.Repository;
using eStore.Helpers;
using eStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTablesController : ControllerBase
    {
        private readonly IOrderTableRepository _orderTableRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IValidation _validation;

        public OrderTablesController(IOrderTableRepository orderTableRepository, IOrderItemRepository orderItemRepository, IShoppingCartRepository shoppingCartRepository, IProductsRepository productsRepository, ICartItemRepository cartItemRepository, IValidation validation)
        {
            _orderTableRepository = orderTableRepository;
            _orderItemRepository = orderItemRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _productsRepository = productsRepository;
            _validation = validation;
            _cartItemRepository = cartItemRepository;
        }

        [HttpPost("CreateOrderTable")]
        /*[Authorize(Roles = AppRole.Customer)]*/
        public async Task<IActionResult> CreateOrderTable([FromForm] string userId)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserId(userId);
            OrderTableModel orderTableModel = new OrderTableModel
            {
                OrderTableId = await _validation.GenerateUniqueId("OrderTable"),
                UserId = shoppingCart.User.Id,
                UserPhoneNumber = shoppingCart.User.PhoneNumber,
                UserAddress = shoppingCart.User.Address,
                Status = "Waiting For Delivery"
            };

            try
            {
                await _orderTableRepository.CreateOrderTable(orderTableModel);

                foreach (var cartItem in shoppingCart.CartItems)
                {
                    OrderItemModel orderItemModel = new OrderItemModel
                    {
                        OrderItemId = await _validation.GenerateUniqueId("OrderItem"),
                        OrderTableId = orderTableModel.OrderTableId,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                    };
                    await _orderItemRepository.CreateOrderItem(orderItemModel);
                }

                var productsToUpdate = shoppingCart.CartItems.Select(cartItem => cartItem.Product).ToList();
                await _productsRepository.UpdateListProducts(productsToUpdate);

                // Remove cart items
                /*await _cartItemRepository.RemoveListCartItemsByShoppingCartId(shoppingCart.ShoppingCartId);*/
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
