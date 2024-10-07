using eStore.DataAccess;

namespace eStore.Models
{
    public class CartItemModel
    {
        public int CartItemId { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual ProductModel Product { get; set; } = null!;
        public virtual ShoppingCartModel ShoppingCart { get; set; } = null!;
    }
}
