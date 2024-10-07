using eStore.DataAccess;
using MailKit.Search;

namespace eStore.Models
{
    public class OrderTableModel
    {
        public OrderTableModel()
        {
            OrderItems = new HashSet<OrderItemModel>();
            OrderAt = DateTime.UtcNow;
        }

        public int OrderTableId { get; set; }
        public string UserId { get; set; } = null!;
        public string UserPhoneNumber { get; set; } = null!;
        public string UserAddress { get; set; } = null!;
        public DateTime OrderAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual ApplicationUserModel User { get; set; } = null!;
        public virtual ICollection<OrderItemModel> OrderItems { get; set; }
    }
}
