using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using eStore.Helpers;
using eStore.DataAccess.Interface;

namespace eStore.Controllers
{
    [Route("api/shopping-cart")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartsController(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        [HttpPost("GetShoppingCartByUserId")]
        /*[Authorize(Roles = AppRole.Customer)]*/
        public async Task<IActionResult> GetShoppingCartByUserId([FromForm] string userId)
        {
            var shoppingCart = await _shoppingCartRepository.GetShoppingCartByUserId(userId);
            return shoppingCart == null ? NotFound() : Ok(shoppingCart);
        }
    }
}
