using Microsoft.AspNetCore.Identity;

namespace eStore.DataAccess
{
    public partial class OrderTable
    {
        public OrderTable()
        {
            OrderItems = new HashSet<OrderItem>();
            OrderAt = DateTime.UtcNow;
        }

        public int OrderTableId { get; set; }
        public string UserId { get; set; } = null!;
        public string UserPhoneNumber { get; set; } = null!;
        public string UserAddress { get; set; } = null!;
        public DateTime OrderAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
