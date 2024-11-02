using eStore.DataAccess.Interface;
using eStore.DataAccess.Repository;
using eStore.Helpers;
using eStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository accountRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IValidation validation;

        public AccountsController(IAccountRepository accountRepository, IShoppingCartRepository shoppingCartRepository, IValidation validation)
        {
            this.accountRepository = accountRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.validation = validation;
        }
        [HttpPost("SignUp")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            var result = await accountRepository.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                try
                {
                    var user = await accountRepository.GetUserAsync(new SignInModel
                    {
                        Email = signUpModel.Email,
                        Password = signUpModel.Password
                    });
                    ShoppingCartModel shoppingCartModel = new ShoppingCartModel
                    {
                        ShoppingCartId = await validation.GenerateUniqueId("ShoppingCart"),
                        UserId = user.Id
                    };
                    await shoppingCartRepository.CreateShoppingCart(shoppingCartModel);
                }catch (Exception ex)
                {
                    StatusCode(500);
                }
            }
            if (result.Succeeded)
            {

                return Ok(result.Succeeded);
            }
            return StatusCode(500);
        }
        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signinModel)
        {
            var result = await accountRepository.SignInAsync(signinModel);
            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }
            return Ok(result);
        }
        [HttpPost("GetUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser([FromForm] SignInModel signinModel)
        {
            var user = await accountRepository.GetUserAsync(signinModel);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }
        [HttpGet("GetAllRentalProviders")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllRentalProviders()
        {
            var user = await accountRepository.GetAllRentalProvidersAsync();
            return Ok(user);
        }
        [HttpPost("ActiveRentalProviderById")]
        [AllowAnonymous]
        public async Task<IActionResult> ActiveRentalProviderById([FromBody] string id)
        {
            try
            {
                await accountRepository.ActiveRentalProviderById(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
