using eStore.Models;

namespace eStore.DataAccess.Interface
{
    public interface ICartItemRepository
    {
        Task<List<CartItemModel>> GetCartItemsByShoppingCartId(int shoppingCartId);
        Task<CartItemModel> GetCartItemByCartItemId(int cartItemId);
        Task AddToCart(CartItemModel cartItemModel);
        Task<List<CartItemModel>> GetAllCartItems();
        Task IncreaseQuantity(CartItemModel cartItemModel);
        Task DecreaseQuantity(CartItemModel cartItemModel);
        Task RemoveCartItem(int cartItemId);
        Task<CartItemModel> CheckAvailableCart(string userId, int productId);
        Task AddQuantity(CartItemModel cartItemModel, int additionalQuantity, int maxQuantity);
        Task RemoveListCartItemsByShoppingCartId(int shoppingCartId);
    }
}
