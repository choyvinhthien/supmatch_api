using eStore.Models;

namespace eStore.DataAccess.Interface
{
    public interface IShoppingCartRepository
    {
        Task CreateShoppingCart(ShoppingCartModel shoppingCart);
        Task<List<ShoppingCartModel>> GetAllShoppingCarts();
        Task<ShoppingCartModel> GetShoppingCartByUserId(string userId);
    }
}
