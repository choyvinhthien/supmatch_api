using Microsoft.AspNetCore.Identity;

namespace eStore.DataAccess
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int ShoppingCartId { get; set; }
        public string UserId { get; set; } = null!;
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
