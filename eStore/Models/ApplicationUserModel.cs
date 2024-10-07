using eStore.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace eStore.Models
{
    public class ApplicationUserModel : IdentityUser
    {
        public ApplicationUserModel()
        {
            OrderTables = new HashSet<OrderTableModel>();
            Feedbacks = new HashSet<FeedbackModel>();
        }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Status { get; set; }
        public virtual ICollection<OrderTableModel> OrderTables { get; set; }
        public virtual ShoppingCartModel ShoppingCarts { get; set; } = null!;
        public virtual ICollection<FeedbackModel> Feedbacks { get; set; }
    }
}
