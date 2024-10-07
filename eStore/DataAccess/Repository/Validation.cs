
using eStore.DataAccess.Interface;

namespace eStore.DataAccess.Repository
{
    public class Validation : IValidation
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IOrderTableRepository _orderTableRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly eStoreContext _context;
        public Validation(IProductsRepository productsRepository, IProductImageRepository productImageRepository, IShoppingCartRepository shoppingCartRepository, ICartItemRepository cartItemRepository, IOrderTableRepository orderTableRepository, IOrderItemRepository orderItemRepository, eStoreContext context)
        {
            _productsRepository = productsRepository;
            _productImageRepository = productImageRepository;
            _cartItemRepository = cartItemRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _orderTableRepository = orderTableRepository;
            _orderItemRepository = orderItemRepository;
            _context = context;
        }
        public async Task<int> LastIdInTable(string tableName)
        {
            if (tableName.Equals("Product"))
            {
                if (_context.Products.Count() != 0)
                    return (await _productsRepository.GetAllProducts()).Max(product => product.ProductId);
                else return 0;
            }
            else if (tableName.Equals("ShoppingCart"))
            {
                if (_context.ShoppingCarts.Count() != 0)
                    return (await _shoppingCartRepository.GetAllShoppingCarts()).Max(shoppingCart => shoppingCart.ShoppingCartId);
                else return 0;
            }
            else if (tableName.Equals("CartItem"))
            {
                if (_context.CartItems.Count() != 0)
                    return (await _cartItemRepository.GetAllCartItems()).Max(cartItem => cartItem.CartItemId);
                else return 0;
            }
            else if (tableName.Equals("OrderTable"))
            {
                if (_context.OrderTables.Count() != 0)
                    return (await _orderTableRepository.GetAllOrderTables()).Max(orderTable => orderTable.OrderTableId);
                else return 0;
            }
            else if (tableName.Equals("OrderItem"))
            {
                if (_context.OrderItems.Count() != 0)
                    return (await _orderItemRepository.GetAllOrderItems()).Max(orderItem => orderItem.OrderItemId);
                else return 0;
            }
            return 0;
        }
        public async Task<int> GenerateUniqueId(string tableName)
        {
            int id = await LastIdInTable(tableName)+1;
            return id;
        }
    }
}
