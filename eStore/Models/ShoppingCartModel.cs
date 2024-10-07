using eStore.DataAccess;

namespace eStore.Models
{
    public class ShoppingCartModel
    {
        public ShoppingCartModel()
        {
            CartItems = new HashSet<CartItemModel>();
        }
        public int ShoppingCartId { get; set; }
        public string UserId { get; set; } = null!;

        public virtual ApplicationUserModel User { get; set; } = null!;
        public virtual ICollection<CartItemModel> CartItems { get; set; }
    }
}
